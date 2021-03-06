﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using AppleUsed.Web.Models.ViewModels.PurchasesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class AdministrationPurchasesController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public IPurchasesService _purchasesService;

        public AdministrationPurchasesController(
            UserManager<ApplicationUser> userManager, 
            IPurchasesService purchasesService)
        {
            _userManager = userManager;
            _purchasesService = purchasesService;
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
        public async Task<IActionResult> CreatePurchase(int adId, int serviceId)
        {
            var model = new PurchaseDetailsViewModel();
            model.PurhcaseDetail.AdId = adId;
            model.PurhcaseDetail.ServicesId = serviceId;
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewPurchase(PurchaseDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Details", model);

            var operationDetails = await _purchasesService.CreatePurchase(model.PurhcaseDetail);
            if(!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePurchase(int id)
        {
            var model = new PurchaseDetailsViewModel();
            var operationDetails = await _purchasesService.GetPurchaseById(id);
            if (!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            model.PurhcaseDetail = operationDetails.Property;

            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdatedPurchase(PurchaseDetailsViewModel model)
        {
            if(!ModelState.IsValid)
                return View("Details", model);

            var operationDetails = await _purchasesService.UpdatePurchase(model.PurhcaseDetail);
            if(!operationDetails.Succedeed)
                return View("Details", model.StatusMessage = operationDetails.Message);

            return RedirectToAction("Index");
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _purchasesService = null;
                    _userManager.Dispose();
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