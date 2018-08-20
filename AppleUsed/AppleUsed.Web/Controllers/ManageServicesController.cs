using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class ManageServicesController : Controller
    {
        public readonly IServicesService _servicesService;

        public ManageServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            var model = new ServicesIndexViewModel();
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