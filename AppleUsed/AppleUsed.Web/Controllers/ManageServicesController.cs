using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class ManageServicesController : Controller
    {
        private IServicesService _servicesService;
        private IServiecActiveTimeService _serviecActiveTimeService;
        private IAdService _adService;
        private PrepearingModelHelper _prepearingModelHelper;

        public ManageServicesController(
            IServicesService servicesService,
            IServiecActiveTimeService serviecActiveTimeService, 
            IAdService adService)
        {
            _servicesService = servicesService;
            _adService = adService;
            _prepearingModelHelper = new PrepearingModelHelper(null);
            _serviecActiveTimeService = serviecActiveTimeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            var model = new ServicesIndexViewModel() { Ad = new AdDTO()};

            var operationDetails = await _adService.GetAdById(id);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            model.Ad = operationDetails.Property;
            model.SelectedAdId = id;
            model.Services = await _servicesService.GetAllServices().ToListAsync();
            model = _prepearingModelHelper.ConfigServicesIndexViewModel(model);

            return View("Index", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> GetServicePriceById(int selectedServiceActiveTimeId)
        {
            var operationDetails = await _serviecActiveTimeService.GetServiceActiveTimesById(selectedServiceActiveTimeId);
            if (!operationDetails.Succedeed)
                return Json(null);

            return Json(operationDetails.Property.Cost);
        }

        public async Task<IActionResult> GetService(int id)
        {
            var model = new ServiceDetailsViewModel();

            var operationDetails = await _servicesService.GetServiceById(id);
            if (!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            model.ServiceDetail = operationDetails.Property;

            return View("Details", model);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService = null;
                    _prepearingModelHelper = null;
                    _servicesService = null;
                    _serviecActiveTimeService = null;
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