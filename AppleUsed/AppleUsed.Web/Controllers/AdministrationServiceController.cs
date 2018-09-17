using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.Web.Helpers;
using AppleUsed.Web.Models.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class AdministrationServiceController : Controller
    {
        public IServicesService _servicesService;
        private PrepearingModelHelper _prepearingModelHelper;

        public AdministrationServiceController(IServicesService servicesService)
        {
            _servicesService = servicesService;
            _prepearingModelHelper = new PrepearingModelHelper();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ServicesIndexViewModel();
            model.Services = await _servicesService.GetAllServices().ToListAsync();
            model = _prepearingModelHelper.ConfigServicesIndexViewModel(model);
            return View("Index", model);
        }

        public async Task<IActionResult> GetService(int id)
        {
            var model = new ServiceDetailsViewModel();
            var operationDetails = await _servicesService.GetServiceById(id);

            if (!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            model.ServiceDetail = operationDetails.Property;
            model = _prepearingModelHelper.ConfigServiceDetailsViewModel(model);

            return View("Details", model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            var model = new ServiceDetailsViewModel()
            {
                ServiceActiveSevenDays = new ServiceActiveTimesDTO { DaysOfActiveService = 7 },
                ServiceActiveTwoWeeks = new ServiceActiveTimesDTO { DaysOfActiveService = 14 },
                ServiceActiveMonth = new ServiceActiveTimesDTO { DaysOfActiveService = 30 }
            };

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
            model = _prepearingModelHelper.ConfigServiceDetailsViewModel(model);

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
                var serviceActiveTimesList = new List<ServiceActiveTime>()
                {
                      new ServiceActiveTime
                    {
                        ServiceActiveTimeId = model.ServiceActiveMonth.ServiceActiveTimeId,
                        Cost = model.ServiceActiveSevenDays.Cost,
                        DaysOfActiveService = model.ServiceActiveSevenDays.DaysOfActiveService,
                        ServiceId = model.ServiceActiveSevenDays.ServiceId
                    },
                      new ServiceActiveTime
                    {
                        ServiceActiveTimeId = model.ServiceActiveTwoWeeks.ServiceActiveTimeId,
                        Cost = model.ServiceActiveTwoWeeks.Cost,
                        DaysOfActiveService = model.ServiceActiveTwoWeeks.DaysOfActiveService,
                        ServiceId = model.ServiceActiveTwoWeeks.ServiceId
                    },
                      new ServiceActiveTime
                    {
                         ServiceActiveTimeId = model.ServiceActiveMonth.ServiceActiveTimeId,
                        Cost = model.ServiceActiveMonth.Cost,
                        DaysOfActiveService = model.ServiceActiveMonth.DaysOfActiveService,
                        ServiceId = model.ServiceActiveMonth.ServiceId
                    }
                };

                model.ServiceDetail.ServiceActiveTimes.AddRange(serviceActiveTimesList);
                operationDetails = await _servicesService.UpdateService(model.ServiceDetail);
            }
            else
            {
                var serviceActiveTimesList = new List<ServiceActiveTime>()
                {
                    new ServiceActiveTime
                    {
                        Cost = model.ServiceActiveSevenDays.Cost,
                        DaysOfActiveService = model.ServiceActiveSevenDays.DaysOfActiveService
                    },
                     new ServiceActiveTime
                    {
                        Cost = model.ServiceActiveTwoWeeks.Cost,
                        DaysOfActiveService = model.ServiceActiveTwoWeeks.DaysOfActiveService
                    },
                      new ServiceActiveTime
                    {
                        Cost = model.ServiceActiveMonth.Cost,
                        DaysOfActiveService = model.ServiceActiveMonth.DaysOfActiveService
                    }
                };

                model.ServiceDetail.ServiceActiveTimes.AddRange(serviceActiveTimesList);
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
            model.Services = getAllServicesResult.ToList();

            return View("Index", model);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _servicesService = null;
                    _prepearingModelHelper = null;
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