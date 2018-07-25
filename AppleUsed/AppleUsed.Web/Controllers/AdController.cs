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
        
        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter, AdIndexViewModel model, int page = 1)
        {
            int pageSize = 5;

            IQueryable<AdDTO> adList;

            adList = await GetFilteredData(model);

            if (!string.IsNullOrEmpty(titleFilter))
            {
                adList = adList.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower()));
            }

            string selectedProductType = adList.Select(a => a.SelectedProductType).FirstOrDefault();

            model = await PrepearingDataForAdIndex(adList, selectedProductType);
            model.Filter.SelectedProductType = selectedProductType;

            int count = adList.Count();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            model.PageViewModel = pageViewModel;
            model.AdList = model.AdList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        public async Task<IQueryable<AdDTO>> GetFilteredData(AdIndexViewModel model)
        {

            var adList = await _adService.GetAds();

            if (model.Filter != null)
            {
                var ads = new List<AdDTO>();

                if (String.IsNullOrEmpty(model.Filter.PriceFilterFrom))
                    model.Filter.PriceFilterFrom = "0";

                if (String.IsNullOrEmpty(model.Filter.PriceFilterTo))
                    model.Filter.PriceFilterTo = adList.Max(x => x.Price).ToString();

                var selectedByModels = model.Filter.ProductsModelFilters.Where(x => x.Selected).AsQueryable();
                var selectedByMemories = model.Filter.ProductMemmories.Where(x => x.Selected).AsQueryable();
                var selectedByColors = model.Filter.ProductsColors.Where(x => x.Selected).AsQueryable();

                if (selectedByModels.Count() > 0)
                {
                    var byModels = (from ad in adList
                                    join smo in selectedByModels on ad.SelectedProductModelId equals smo.Id
                                    select ad).ToList();

                    ads.AddRange(byModels);
                }

                if (selectedByMemories.Count() > 0)
                {
                    var byMemories = (from ad in adList
                                      join sm in selectedByMemories on ad.SelectedPoductMemoryId equals sm.Id
                                      select ad).ToList();

                    if (selectedByModels.Count() > 0)
                        ads.Union(byMemories).Distinct();
                    else
                        ads.Except(byMemories);
                }


                if (selectedByColors.Count() > 0)
                {
                    var byColors = (from ad in adList
                                    join sc in selectedByColors on ad.SelectedProductColorId equals sc.Id
                                    where ad.SelectedProductType == model.Filter.SelectedProductType
                                    select ad).ToList();

                    if (selectedByMemories.Count() > 0)
                        ads.Union(byColors).Distinct();
                    else
                        ads.Except(byColors);
                }

                if (selectedByModels.Count() == 0 && selectedByColors.Count() == 0 && selectedByMemories.Count() == 0)
                {
                    ads = await adList.Where(x => x.SelectedProductType == model.Filter.SelectedProductType).ToListAsync();
                }

                var filteringWithPrice = ads.Where(x => x.Price > decimal.Parse(model.Filter.PriceFilterFrom)
                    && x.Price < decimal.Parse(model.Filter.PriceFilterTo)).ToList();

                ads = new List<AdDTO>();
                ads.AddRange(filteringWithPrice);

                return ads.AsQueryable();

            }


            return adList;
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