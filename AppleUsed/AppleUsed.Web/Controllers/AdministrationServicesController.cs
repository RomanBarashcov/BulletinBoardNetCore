using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class AdministrationServicesController : Controller
    {
        public readonly IServicesService _servicesService;

        public AdministrationServicesController(IServicesService servicesService)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveService(ServiceDetailsViewModel model)
        {
            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            if (!ModelState.IsValid)
                return View("Details", model);

            if (model.ServiceDetail.ServicesId > 0)
            {
                operationDetails = await _servicesService.UpdateService(model.ServiceDetail);
            }
            else
            {
                operationDetails = await _servicesService.CreateService(model.ServiceDetail);
            }

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
                model.StatusMessage = operationDetails.Message;
            }

            var getAllServicesResult = _servicesService.GetAllServices();
            model.Services = getAllServicesResult;

            return View("Index", model);
        }
    }
}