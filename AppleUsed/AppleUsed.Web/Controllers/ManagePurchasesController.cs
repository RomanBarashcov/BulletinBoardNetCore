using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using AppleUsed.Web.Models.ViewModels.PurchasesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ManagePurchasesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IPurchasesService _purchasesService;
        public readonly IServicesService _servicesService;
        public readonly IServiecActiveTimeService _serviecActiveTimeService;

        public ManagePurchasesController(
            UserManager<ApplicationUser> userManager,
            IPurchasesService purchasesService,
            IServicesService servicesService,
            IServiecActiveTimeService serviecActiveTimeService)
        {
            _userManager = userManager;
            _purchasesService = purchasesService;
            _servicesService = servicesService;
            _serviecActiveTimeService = serviecActiveTimeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PurchasesIndexViewModel();
            model.Purchases = _purchasesService.GetPurchases();
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchase(int id)
        {
            var model = new PurchaseDetailsViewModel();
            var operationDetails = await _purchasesService.GetPurchaseById(id);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            return View("Details", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchasesByUser()
        {
            var model = new PurchasesIndexViewModel();
            string userId = _userManager.GetUserId(User);

            var operationDetails = await _purchasesService.GetPurchaseByUserId(userId);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            model.Purchases = operationDetails.Property;

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetActivePurchasesByUser()
        {
            var model = new PurchasesIndexViewModel();
            string userId = _userManager.GetUserId(User);

            var operationDetails = await _purchasesService.GetPurchaseByUserId(userId);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            model.Purchases = operationDetails.Property.Where(x => x.IsActive == true);

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeactivatedPurchasesByUser()
        {
            var model = new PurchasesIndexViewModel();
            string userId = _userManager.GetUserId(User);

            var operationDetails = await _purchasesService.GetPurchaseByUserId(userId);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            model.Purchases = operationDetails.Property.Where(x => x.IsActive == false);

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePurchase(int adId, int serviceId, int serviceActiveId)
        {
            var model = new PurchaseDetailsViewModel()
            {
                PurhcaseDetail = new PurchaseDTO(), SelectedService = new ServiceDTO()
            };

            var serviceActiveTimes = await _serviecActiveTimeService.GetServiceActiveTimesById(serviceActiveId);
            if (!serviceActiveTimes.Succedeed)
                return View("Details", model.StatusMessage = serviceActiveTimes.Message);

            var service = await _servicesService.GetServiceById(serviceActiveTimes.Property.ServiceId);

            model.PurhcaseDetail.AdId = adId;
            model.PurhcaseDetail.StartDateService = DateTime.Now;
            model.PurhcaseDetail.EndDateService = DateTime.Now.AddDays(serviceActiveTimes.Property.DaysOfActiveService);
            model.PurhcaseDetail.ServicesId = serviceId;
            model.PurhcaseDetail.TotalCost = serviceActiveTimes.Property.Cost;
            model.SelectedService = service.Property;

            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewPurchase(PurchaseDetailsViewModel model)
        {
            if(model.PurhcaseDetail == null || !ModelState.IsValid)
                return View("Details", model);

            var operationDetails = await _purchasesService.CreatePurchase(model.PurhcaseDetail);
            if (!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            return RedirectToAction("Index");
        }
    }
}