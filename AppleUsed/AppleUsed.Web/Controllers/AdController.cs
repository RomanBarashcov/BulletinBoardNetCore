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

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IActionResult> Index()
        {
            List<AdDTO> model =  await _adService.GetAds();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAd()
        {
            var adDTO = await _adService.GetDataForCreatingAd();
            AdViewModel model = new AdViewModel { AdDTO = adDTO };

            //model.CityAreasSelectList = new SelectList(model.AdDTO.CityAreasList, "CityAreaId", "Name");
            //model.CityesSelectList = new SelectList(model.AdDTO.CityesList, "CityId", "Name");
            model.ProductTypesSelectList = new SelectList(model.AdDTO.ProductTypesList, "ProductTypesId", "Name");
            model.ProductMemoriesSelectList = new SelectList(model.AdDTO.ProductMemoriesList, "ProductMemoriesId", "Name");
            model.ProductColorsSelectList = new SelectList(model.AdDTO.ProductColorsList, "ProductColorsId", "Name");
            model.ProductStatesSelectList = new SelectList(model.AdDTO.ProductStatesList, "ProductStatesId", "Name");

            return View(model);
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

            return View();
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
    }
}