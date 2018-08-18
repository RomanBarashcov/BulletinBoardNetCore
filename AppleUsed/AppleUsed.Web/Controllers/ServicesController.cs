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
    public class ServicesController : Controller
    {
        public readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ServicesIndexViewModel();
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

        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            var model = new ServiceDetailsViewModel();
            return View("Details", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewService(ServiceDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateService", model);

            var operationDetail = await _servicesService.CreateService(model.ServiceDetail);
            if (!operationDetail.Succedeed)
                return View("CreateService", model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var model = new ServiceDetailsViewModel();
            var operationDetails = await _servicesService.GetServiceById(id);

            if (!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            model.ServiceDetail = operationDetails.Property;

            return View("Details", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdatedService(ServiceDetailsViewModel model)
        {
            if(!ModelState.IsValid)
                return View("Details", model);

            var operationDetails = await _servicesService.UpdateService(model.ServiceDetail);
            if(!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            var model = new ServicesIndexViewModel();
            var operationDetails = await _servicesService.DeleteService(id);

            if (!operationDetails.Succedeed)
            {
                var getAllServicesResult = _servicesService.GetAllServices();
                model.Services = getAllServicesResult;
                model.StatusMessage = operationDetails.Message;
            }

            return View("Index", model);
        }
    }
}