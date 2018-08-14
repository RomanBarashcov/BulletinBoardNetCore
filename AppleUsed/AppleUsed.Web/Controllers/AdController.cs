using System;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Filters;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace AppleUsed.Web.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdService _adService;
        private readonly PrepearingModel _prepearingModel;
        private readonly ICityService _cityService;
        private readonly IProductModelsService _productModelsService;

        public AdController(
            IAdService adService, 
            ICityService cityService, 
            IProductModelsService productModelsService)
        {
            _adService = adService;
            _prepearingModel = new PrepearingModel(_adService);
            _cityService = cityService;
            _productModelsService = productModelsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string titleFilter, string cityFilter, string adType, AdIndexViewModel model, int page = 1)
        {
            int pageSize = 5;

            var result = await _adService.GetAds();
            if (!result.Succedeed)
                return View(model);

            IQueryable<AdDTO> adList = result.Property;

            if(model.SortViewModel != null)
                adList = new SelectedOptionFilter(model.SortViewModel.SelectedOptionValue, adList).SelectedOptionChanged();

            adList = await new CheckBoxFilter(model, adList).GetFilteredAdsData();
            adList = new ButtonAreaFilter(adType, adList).GetFilteredAdsData();

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
                model.SortViewModel.SortOptionList = _prepearingModel.GetSelectionOptionsList();
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

            model.AddDetails = getByIdReult.Property;

            var similarAdsResult = await _adService.GetAdsByProductTypeId(model.AddDetails.SelectedProductTypeId);
            if(!similarAdsResult.Succedeed)
                return View(model);

            var similarAds = similarAdsResult.Property;
            model.SimilarAds = await similarAds.Take(4).ToListAsync();

            var otherAdsByAuthorResult = await _adService.GetAdsByUserId(model.AddDetails.User.Id);

            if(!otherAdsByAuthorResult.Succedeed)
                return View(model);

            var otherAdsByAuthor = otherAdsByAuthorResult.Property;
            model.OtherAdsByAuthor = await otherAdsByAuthor.Take(5).ToListAsync();

            return View(model);
        }

    }
}