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

        public AdController(IAdService adService)
        {
            _adService = adService;
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
                model = await PrepearingDataForAdIndex(adList, selectedProductType);
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
            var adDTO = await _adService.GetDataForCreatingAdOrDataForFilter();
            AdViewModel model = new AdViewModel { AdDTO = adDTO };

            //model.CityAreasSelectList = new SelectList(model.AdDTO.CityAreasList, "CityAreaId", "Name");
            //model.CityesSelectList = new SelectList(model.AdDTO.CityesList, "CityId", "Name");
            model.ProductTypesSelectList = new SelectList(model.AdDTO.ProductTypesList, "ProductTypesId", "Name");
            model.ProductMemoriesSelectList = new SelectList(model.AdDTO.ProductMemoriesList, "ProductMemoriesId", "Name");
            model.ProductColorsSelectList = new SelectList(model.AdDTO.ProductColorsList, "ProductColorsId", "Name");
            model.ProductStatesSelectList = new SelectList(model.AdDTO.ProductStatesList, "ProductStatesId", "Name");

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

            var result = await _adService.SaveAdAsync(userName, model.AdDTO, model.Photos);
            if (!result.Succedeed)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return View("Index");
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

        private async Task<AdIndexViewModel> PrepearingDataForAdIndex(IQueryable<AdDTO> ads, string selectedProductType)
        {
            AdIndexViewModel adIndexViewModel = new AdIndexViewModel { AdList = ads.ToList() };
            AdDTO dataForFilter = await _adService.GetDataForCreatingAdOrDataForFilter();

            adIndexViewModel.Filter = new FilterViewModel();

            adIndexViewModel.Filter.ProductsModelFilters = new List<ProductsModelFilter>();
            adIndexViewModel.Filter.ProductMemmories = new List<ProductMemmoriesFilter>();
            adIndexViewModel.Filter.ProductsColors = new List<ProductsColorFilter>();

            var productModelsList = dataForFilter.ProductModelsList.Where(p => p.ProductTypes.Name == selectedProductType).OrderByDescending(x => x.Name).ToList();

            for (int i = 0; i <= productModelsList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductsModelFilters.Add(
                    new ProductsModelFilter
                    {
                        Id = productModelsList[i].ProductModelsId,
                        Name = productModelsList[i].Name
                    });
            }

            var productMemmoriesList = dataForFilter.ProductMemoriesList.OrderBy(x => x.ProductMemoriesId).ToList();

            for (int i = 0; i <= productMemmoriesList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductMemmories.Add(
                    new ProductMemmoriesFilter
                    {
                        Id = productMemmoriesList[i].ProductMemoriesId,
                        Name = productMemmoriesList[i].Name
                    });
            }

            var productColorsList = dataForFilter.ProductColorsList.OrderBy(x => x.ProductColorsId).ToList();

            for (int i = 0; i <= productColorsList.Count - 1; i++)
            {
                adIndexViewModel.Filter.ProductsColors.Add(
                    new ProductsColorFilter
                    {
                        Id = productColorsList[i].ProductColorsId,
                        Name = productColorsList[i].Name
                    });
            }

            return adIndexViewModel;
        }


    }
}