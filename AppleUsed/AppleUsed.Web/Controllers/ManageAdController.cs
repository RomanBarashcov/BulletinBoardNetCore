using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using AppleUsed.Web.Models.ViewModels.ManageAdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppleUsed.Web.Controllers
{
    public class ManageAdController : Controller
    {
        private IAdService _adService;
        private IAdUpService _adUpService;
        private IAdViewsService _adViewsService;
        private ICityService _cityService;
        private IProductModelsService _productModelsService;
        private PrepearingModelHelper _prepearingModel;

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
            _prepearingModel = new PrepearingModelHelper(_adService);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int adStatusId = 1, int page = 1)
        {
            int pageSize = 5;
            string userName = User.Identity.Name;
            ManageAdIndexViewModel model = new ManageAdIndexViewModel
            { AdList = new List<AdDTO>(), SelectedAdStatus = adStatusId };

            var getAdsByUserResult = await _adService.GetAdsByUserName(userName);
            if (!getAdsByUserResult.Succedeed)
                return View(new List<AdDTO>());

            if(adStatusId == (int)AdStatuses.InProgress || adStatusId == (int)AdStatuses.Deactivated)
            {
                model.AdList = getAdsByUserResult.Property.Where(x => x.AdStatusId == adStatusId).ToList();
            }
            else
            {
                model.AdList = getAdsByUserResult.Property.Where(x => x.AdStatusId == adStatusId && x.IsModerate).ToList();
            }

            int count = model.AdList.Count();
            model.PageViewModel = new PageViewModel(count, page, pageSize);
            model.AdList = model.AdList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
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


            var dataForSelectList = _adService.GetDataForCreatingAdOrDataForFilter();

            model = _prepearingModel.PrepearingAdViewModel(
                dataForSelectList.citiesDTO,
                dataForSelectList.cityAreasDTO,
                dataForSelectList.productTypesDTO,
                dataForSelectList.productModelsDTO,
                dataForSelectList.productMemoriesDTO,
                dataForSelectList.productColorsDTO,
                dataForSelectList.productStateDTO,
                new AdDTO());

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

            var result = await _adService.SaveAd(
                userName,
                model.AdDTO,
                model.Photos);

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
                var result = await _adService.SetStatusAd(id, (int)AdStatuses.Activated);
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
                var result = await _adService.SetStatusAd(id, (int)AdStatuses.Deactivated);
                if (!result.Succedeed)
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return RedirectToActionPermanent("Index");
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService = null;
                    _adService = null;
                    _adUpService = null;
                    _adViewsService = null;
                    _cityService = null;
                    _productModelsService = null;
                    _prepearingModel = null;
                 }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}