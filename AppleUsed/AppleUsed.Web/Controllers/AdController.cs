using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Extensions;
using AppleUsed.Web.Filters;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller, IDisposable
    {
        private IAdService _adService;
        private readonly PrepearingModel _prepearingModel;

        public AdController(IAdService adService)
        {
            _adService = adService;
            _prepearingModel = new PrepearingModel(_adService);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter, string adType, AdIndexViewModel model, int page = 1)
        {
            int pageSize = 5;

            IQueryable<AdDTO> adList = await _adService.GetAds();

            adList = await new CheckBoxFilter(model, adList).GetFilteredAdsData();

            if (!string.IsNullOrEmpty(titleFilter))
            {
                adList = adList.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower()));
            }

            if(model.Filter == null)
            {
                string selectedProductType = adList.Select(a => a.SelectedProductType).FirstOrDefault();
                model = await _prepearingModel.PrepearingAdIndexViewModel(adList, selectedProductType);
                model.Filter.SelectedProductType = selectedProductType;
            }
            else
            {
                model.AdList = adList.ToList();
            }
           
            int count = adList.Count();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            model.PageViewModel = pageViewModel;
            model.AdList = model.AdList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAd()
        {
            var dataForSelectList = await _adService.GetDataForCreatingAdOrDataForFilter();
            var model = _prepearingModel.PrepearingAdViewModel(dataForSelectList, new AdDTO());
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult GetProductModelsSelectList()
        {
            AdDTO model = new AdDTO();

            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {

                        var obj = JsonConvert.DeserializeObject<AdDTO>(requestBody);
                        if (obj != null)
                        {
                            model = obj;
                        }
                    }
                }
            }

            int selectedProductTypeId = Convert.ToInt32(model.SelectedProductType);

            return Json(new SelectList(model.ProductModelsList.Where(x => x.ProductTypes.ProductTypesId == selectedProductTypeId), "ProductModelsId", "Name"));
        }


        [HttpPost]
        public async Task<IActionResult> SaveAd(AdViewModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateAd", model);

            string userName = User.Identity.Name;

            var result = await _adService.SaveAd(userName, model.AdDTO, model.Photos);
            if (!result.Succedeed)
            {
                ModelState.AddModelError("", result.Message);
                return View("CreateAd", model);
            }
  
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AdDetails(int? id)
        {
            AdDetailsViewModel model = new AdDetailsViewModel();
            model.AddDetails = await _adService.GetAdById(id??0);
            var similarAds = await _adService.GetAdsByProductTypeId(model.AddDetails.SelectedProductTypeId);
            model.SimilarAds = await similarAds.Take(4).ToListAsync();
            var otherAdsByAuthor = await _adService.GetAdsByUserId(model.AddDetails.User.Id);
            model.OtherAdsByAuthor = await otherAdsByAuthor.Take(5).ToListAsync();
            return View(model);
        }


    }
}