using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class ManageServicesController : Controller
    {
        public readonly IServicesService _servicesService;
        public readonly IAdService _adService;

        public ManageServicesController(IServicesService servicesService, IAdService adService)
        {
            _servicesService = servicesService;
            _adService = adService;
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

            return View("Index", model);
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
    }
}