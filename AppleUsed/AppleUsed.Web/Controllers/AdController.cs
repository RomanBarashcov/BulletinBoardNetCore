using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;


namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdService _adService;
        private IQueryable<AdDTO> AdList;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter)
        {
            if(AdList == null || AdList.Count() == 0)
                AdList = await _adService.GetAds();

            if (!string.IsNullOrEmpty(titleFilter))
            {
                AdList = AdList.Where(x => x.Title.ToLower().Contains(titleFilter.ToLower())).AsQueryable();
            }

            string selectedProductType = AdList.Select(a => a.SelectedProductType).FirstOrDefault();

            var model = await PrepearingDataForAdIndex(AdList, selectedProductType);
   

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(AdIndexViewModel model)
        {
            if (model.Filter != null)
            {
                var selectedByModels = model.Filter.ProductsModelFilters.Where(x => x.Selected).ToList();
                var selectedByMemories = model.Filter.ProductMemmories.Where(x => x.Selected).ToList();
                var selectedByColors = model.Filter.ProductsColors.Where(x => x.Selected).ToList();

                AdList = await _adService.GetAds();
                var ads = new List<AdDTO>();

                if (selectedByModels.Count() > 0)
                {
                    for (int i = 0; i <= selectedByModels.Count() - 1; i++)
                    {
                        var result = AdList.Where(s => s.SelectedProductModelId == selectedByModels[i].Id).ToList();
                        ads.AddRange(result);
                    }
                }

                if(selectedByMemories.Count > 0)
                {
                    for (int i = 0; i <= selectedByMemories.Count() - 1; i++)
                    {
                        var result = AdList.Where(s => s.SelectedPoductMemoryId == selectedByMemories[i].Id).ToList();
                        ads.AddRange(result);
                    }
                }

                if(selectedByColors.Count > 0)
                {
                    for (int i = 0; i <= selectedByColors.Count() - 1; i++)
                    {
                        var result = AdList.Where(s => s.SelectedProductColorId == selectedByColors[i].Id).ToList();
                        ads.AddRange(result);
                    }
                }

                var uniqueListResult = (from obj in ads select obj).GroupBy(x => x.AdId).Select(a => a.FirstOrDefault()).ToList();

                ads = new List<AdDTO>();
                ads.AddRange(uniqueListResult);

                model = await PrepearingDataForAdIndex(ads.AsQueryable(), "iPhone");

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