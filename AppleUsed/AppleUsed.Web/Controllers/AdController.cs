using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Extensions;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdService _adService;
        private  IQueryable<AdDTO> AdList;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter, int page = 1)
        {

            int pageSize = 4;
          
            if (AdList == null || AdList.Count() == 0)
                AdList = await _adService.GetAds();

            if (!string.IsNullOrEmpty(titleFilter))
            {
                AdList = AdList.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower())).AsQueryable();
            }

            string selectedProductType = AdList.Select(a => a.SelectedProductType).FirstOrDefault();

            var model = await PrepearingDataForAdIndex(AdList, selectedProductType);


            int count = AdList.Count();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            model.PageViewModel = pageViewModel;

            var source = model.AdList;

            model.AdList = model.AdList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(AdIndexViewModel model)
        {
            if (model.Filter != null)
            {
                if (String.IsNullOrEmpty(model.Filter.PriceFilterFrom))
                    model.Filter.PriceFilterFrom = "0";

                if(String.IsNullOrEmpty(model.Filter.PriceFilterTo))
                    model.Filter.PriceFilterTo = "1000000";

                var selectedByModels = model.Filter.ProductsModelFilters.Where(x => x.Selected).AsQueryable();
                var selectedByMemories = model.Filter.ProductMemmories.Where(x => x.Selected).AsQueryable();
                var selectedByColors = model.Filter.ProductsColors.Where(x => x.Selected).AsQueryable();

                AdList = await _adService.GetAds();

                var ads = new List<AdDTO>();

                if (selectedByModels.Count() > 0 && selectedByMemories.Count() > 0 && selectedByColors.Count() > 0)
                {
                    var adsResult = (from ad in AdList
                                     join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                     join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                     join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);
                }
                else if (selectedByModels.Count() > 0 && selectedByMemories.Count() == 0 && selectedByColors.Count() == 0)
                {

                    var adsResult = (from ad in AdList
                                     join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);

                }
                else if (selectedByModels.Count() > 0 && selectedByMemories.Count() > 0 && selectedByColors.Count() == 0)
                {

                    var adsResult = (from ad in AdList
                                     join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                     join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);

                }
                else if (selectedByModels.Count() == 0 && selectedByMemories.Count() == 0 && selectedByColors.Count() > 0)
                {

                    var adsResult = (from ad in AdList
                                     join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);

                }
                else if (selectedByModels.Count() == 0 && selectedByMemories.Count() > 0 && selectedByColors.Count() > 0)
                {
                    var adsResult = (from ad in AdList
                                     join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                     join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);
                }
                else if (selectedByModels.Count() == 0 && selectedByMemories.Count() > 0 && selectedByColors.Count() == 0)
                {
                    var adsResult = (from ad in AdList
                                     join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);
                }
                else if (selectedByModels.Count() == 0 && selectedByMemories.Count() > 0 && selectedByColors.Count() == 0)
                {
                    var adsResult = (from ad in AdList
                                     join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);
                }
                else if (selectedByModels.Count() > 0 && selectedByMemories.Count() == 0 && selectedByColors.Count() > 0)
                {
                    var adsResult = (from ad in AdList
                                     join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                     join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                     select ad).ToList();

                    ads.AddRange(adsResult);
                }
                else if (selectedByModels.Count() == 0 && selectedByMemories.Count() == 0 && selectedByColors.Count() == 0)
                {
                    ads.AddRange(AdList);
                }

                var uniqueListResult = (from obj in ads select obj).GroupBy(x => x.AdId).Select(a => a.FirstOrDefault()).ToList();


                var filteringWithPrice = uniqueListResult.Where(x => x.Price > decimal.Parse(model.Filter.PriceFilterFrom)
                    && x.Price < decimal.Parse(model.Filter.PriceFilterTo)).ToList();
                
                ads = new List<AdDTO>();
                ads.AddRange(filteringWithPrice);


                string selectedProductType = AdList.Select(a => a.SelectedProductType).FirstOrDefault();
                model = await PrepearingDataForAdIndex(ads.AsQueryable(), selectedProductType);


               


            }

                return View("Index", model);
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
            var model = await _adService.GetAdById(id??0);
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