using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Enums;
using AppleUsed.BLL.Infrastructure;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using AppleUsed.Web.Models.ViewModels.ModerationAdViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ModerationAdController : Controller
    {
        private IAdService _adService;

        public ModerationAdController(IAdService adService)
        {
            _adService = adService;
        }

        public async Task<IActionResult> Index(int adStatus = 2, int page = 1)
        {
            int pageSize = 5;
            ModerationAdIndexViewModel model = new ModerationAdIndexViewModel
            {
                AdList = new List<AdDTO>(),
                SelectedAdStatus = adStatus
            };

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

            int count = model.AdList.Count();
            model.PageViewModel = new PageViewModel(count, page, pageSize);
            model.AdList = model.AdList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService = null;
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