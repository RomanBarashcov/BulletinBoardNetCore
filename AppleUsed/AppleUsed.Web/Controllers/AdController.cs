using System;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller
    {
        private IAdService _adService;
        private PrepearingModelHelper _prepearingModel;
        private AdFilter _adFilter;
        private ICityService _cityService;
        private IProductModelsService _productModelsService;
        private IAdViewsService _adViewsService;

        public AdController(
            IAdService adService, 
            ICityService cityService, 
            IProductModelsService productModelsService,
            IAdViewsService adViewsService)
        {
            _adService = adService;
            _prepearingModel = new PrepearingModelHelper(_adService);
            _adFilter = new AdFilter(_prepearingModel);
            _cityService = cityService;
            _productModelsService = productModelsService;
            _adViewsService = adViewsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter, string productState, AdIndexViewModel model, int page = 1)
        {
            int pageSize = 5;

            var result = await _adService.GetActiveAds();
            if (!result.Succedeed)
                return View(model);


            var topAds = await _adService.GetActiveRandomTopAds();
            IQueryable<AdDTO> adQueryResult = result.Property;
            adQueryResult = await _adFilter.FilteringData(titleFilter, productState, adQueryResult, model);
            model = await _adFilter.PrepearingFilter(adQueryResult, model);
            model.TopAds = topAds.Property.ToList();

            int count = model.SimpleAds.Count();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            model.PageViewModel = pageViewModel;
            model.SimpleAds = model.SimpleAds.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAd()
        {
            var dataForSelectList = _adService.GetDataForCreatingAdOrDataForFilter();
            var model = _prepearingModel.PrepearingAdViewModel(dataForSelectList, new AdDTO());
            return View(model);
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

            var getByIdReult = await _adService.GetAdById(id ?? 0);
            if(!getByIdReult.Succedeed)
                return View(model);

            await _adViewsService.UpdateViewsAd(id ?? 0);
            model.AddDetails = getByIdReult.Property;

            var similarAdsResult = await _adService.GetAdsByProductTypeId(model.AddDetails.SelectedProductTypeId);
            if(!similarAdsResult.Succedeed)
                return View(model);

            var similarAds = similarAdsResult.Property;
            model.SimilarAds = await similarAds.OrderBy(x => Guid.NewGuid()).Take(4).ToListAsync();

            var otherAdsByAuthorResult = await _adService.GetAdsByUserId(model.AddDetails.User.Id);

            if(!otherAdsByAuthorResult.Succedeed)
                return View(model);

            var otherAdsByAuthor = otherAdsByAuthorResult.Property;
            model.OtherAdsByAuthor = await otherAdsByAuthor.Take(5).ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveAdsByUserId(string id)
        {
            if(String.IsNullOrEmpty(id))
                return RedirectToAction("Index");

            var operationDetails = await _adService.GetActiveAdsByUserId(id);
            if(!operationDetails.Succedeed)
                return RedirectToAction("Index");

            UserAdsViewModel model = new UserAdsViewModel
            {
                Ads = operationDetails.Property.ToList(),
                User = new UserDTO
                {
                    Id = operationDetails.Property.FirstOrDefault().User.Id,
                    Email = operationDetails.Property.FirstOrDefault().User.Email,
                    UserName = operationDetails.Property.FirstOrDefault().User.UserName,
                    PhoneNumber = operationDetails.Property.FirstOrDefault().User.PhoneNumber,
                    RegistrationDate = operationDetails.Property.FirstOrDefault().User.RegistrationDate
                }
            };

            return View("UserActiveAds", model);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService.Dispose();
                    _prepearingModel.Dispose();
                    _cityService.Dispose();
                    _productModelsService.Dispose();
                    _adViewsService.Dispose();

                    _adService = null;
                    _prepearingModel = null;
                    _adFilter = null;
                    _cityService = null;
                    _productModelsService = null;
                    _adViewsService = null;
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