using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppleUsed.Web.Controllers
{
    public class ManageAdController : Controller
    {
        private readonly IAdService _adService;
        private readonly IAdUpService _adUpService;
        private readonly IAdViewsService _adViewsService;
        private readonly ICityService _cityService;
        private readonly IProductModelsService _productModelsService;
        private readonly PrepearingModel _prepearingModel;

        [TempData]
        public string StatusMessage { get; set; }

        public ManageAdController(
            IAdService adService,
            IAdUpService adUpService,
            IAdViewsService adViewsService,
            ICityService cityService,
            IProductModelsService productModelsService)
        {
            _adService = adService;
            _adUpService = adUpService;
            _adViewsService = adViewsService;
            _cityService = cityService;
            _productModelsService = productModelsService;
            _prepearingModel = new PrepearingModel(_adService);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int adStatusId = 1)
        {
            string userName = User.Identity.Name;

            var getAdsByUserResult = await _adService.GetAdsByUser(userName);
            if (!getAdsByUserResult.Succedeed)
                return View(new List<AdDTO>());

            List<AdDTO> ads = new List<AdDTO>();

            if(adStatusId == (int)AdStatuses.InProgress || adStatusId == (int)AdStatuses.Deactivated)
            {
                ads = getAdsByUserResult.Property.Where(x => x.AdStatusId == adStatusId).ToList();
            }
            else
            {
                ads = getAdsByUserResult.Property.Where(x => x.AdStatusId == adStatusId && x.IsModerate).ToList();
            }

            return View(ads);
        }

        [HttpGet]
        public async Task<IActionResult> AdUpToList(int id)
        {
            var updateUpAdResult = await _adUpService.UpAd(id);
            if (!updateUpAdResult.Succedeed)
                this.StatusMessage = updateUpAdResult.Message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ResetViews(int id)
        {
            await _adViewsService.ResetViews(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAd(int id)
        {
            AdViewModel model = new AdViewModel();

            var getAdByIdResult = await _adService.GetAdById(id);
            if (!getAdByIdResult.Succedeed)
                return View("Details", model);

            var dataForSelectList = await _adService.GetDataForCreatingAdOrDataForFilter();
            model = _prepearingModel.PrepearingAdViewModel(dataForSelectList, getAdByIdResult.Property);

            ViewBag.AdId = id;
            return View("Details", model);
        }

        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            return Ok();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAd(AdViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Details", model);

            string userName = User.Identity.Name;

            var result = await _adService.SaveAd(userName, model.AdDTO, model.Photos);
            if (!result.Succedeed)
            {
                ModelState.AddModelError("", result.Message);
                return View("Details", model);
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult GetProductModelsSelectList(string selectedProductTypeId)
        {
            int _selectedProductTypeId = Convert.ToInt32(selectedProductTypeId);

            var productModels = _productModelsService.GetProductModels()
                .Where(x => x.ProductTypes.ProductTypesId == _selectedProductTypeId);

            return Json(new SelectList(productModels, "ProductModelsId", "Name"));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult GetAreasCitySelectList(string selectedCityAareaId)
        {
            int _selectedCityAreaId = Convert.ToInt32(selectedCityAareaId);

            var areasCities = _cityService.GetCitiesByCityAreaId(_selectedCityAreaId).Where(x => x.CityArea.CityAreaId == _selectedCityAreaId);

            return Json(new SelectList(areasCities, "CityId", "Name"));
        }

        [HttpGet]
        public async Task<IActionResult> ActivationAd(int? adId = 0)
        {
            int id = adId ?? 0;
            if (id > 0)
            {
                var result = await _adService.ActivationAd(id);
                if (!result.Succedeed)
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeactivationAd(int? adId = 0)
        {
            int id = adId ?? 0;
            if (id > 0)
            {
                var result = await _adService.DeactivationAd(id);
                if (!result.Succedeed)
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return RedirectToActionPermanent("Index");
        }
    }
}