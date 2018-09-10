using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.ModerationAdViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ModerationAdController : Controller
    {
        private readonly IAdService _adService;

        public ModerationAdController(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IActionResult> Index(int adStatus = 2)
        {
            ModerationAdIndexViewModel model = 
                new ModerationAdIndexViewModel { AdList = new List<AdDTO>() };

            if (adStatus == (int)AdStatuses.Activated)
            {
                var getActiveAds = await _adService.GetActiveAds();
                model.AdList = getActiveAds.Property.ToList();
            }
            else if (adStatus == (int)AdStatuses.InProgress)
            {
                var getInProgress = await _adService.GetInProgressAds();
                model.AdList = getInProgress.Property.ToList();
            }
            else if (adStatus == (int)AdStatuses.Deactivated)
            {
                var getDeactivatedAds = await _adService.GetDeactivatedAds();
                model.AdList = getDeactivatedAds.Property.ToList();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SetAdStatus(int adId, int adStatus)
        {
            ModerationAdIndexViewModel model =
               new ModerationAdIndexViewModel { AdList = new List<AdDTO>() };

            OperationDetails<int> operationDetails = new OperationDetails<int>(false, "", 0);

            operationDetails = await _adService.SetStatusAd(adId, adStatus);
            if (!operationDetails.Succedeed)
                return View("Index", model.StatusMessage = operationDetails.Message);

            return RedirectToAction("Index", adStatus);
        }
    }
}