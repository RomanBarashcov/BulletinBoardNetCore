using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        public IActionResult Index()
        {
            var model = _adService.GetAds();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAd()
        {
            var adDTO = await _adService.GetDataForCreatingAd();
            AdViewModel model = new AdViewModel { AdDTO = adDTO };

            //model.CityAreasSelectList = new SelectList(model.AdDTO.CityAreasList, "CityAreaId", "Name");
            //model.CityesSelectList = new SelectList(model.AdDTO.CityesList, "CityId", "Name");
            model.ProductTypesSelectList = new SelectList(model.AdDTO.ProductTypesList, "ProductTypesId", "Name");
            model.ProductModelsSelectList = new SelectList(model.AdDTO.ProductModelsList, "ProductModelsId", "Name");
            model.ProductMemoriesSelectList = new SelectList(model.AdDTO.ProductMemoriesList, "ProductMemoriesId", "Name");
            model.ProductColorsSelectList = new SelectList(model.AdDTO.ProductColorsList, " ProductColorsId", "Name");
            model.ProductStatesSelectList = new SelectList(model.AdDTO.ProductStatesList, "ProductStates", "Name");

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveAd(AdDTO ad)
        {

            return View();
        }
    }
}