using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AppleUsed.BLL.DTO;
using AppleUsed.Web.Models.ViewModels.HomeViewModels;
using System;

namespace AppleUsed.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private ISeedService _seedService;
        private IAdService _adService;

        public HomeController(ISeedService seedService, IAdService adService)
        {
            _seedService = seedService;
            _adService = adService;
        }

        //private readonly IAdService _adService;

        //public HomeController(IAdService adService)
        //{
        //    _adService = adService;
        //}

        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                LatestAds = new List<AdDTO>(),
                VipAds = new List<AdDTO>()
            };

            var latestAdsResult = await _adService.GetActiveAds();
            var vipAds = await _adService.GetActiveRandomVIPAds();

            if(latestAdsResult.Property != null)
                model.LatestAds = latestAdsResult.Property.OrderByDescending(x=>x.AdId).Take(12).ToList();

            if (vipAds.Property != null)
                model.VipAds = vipAds.Property.ToList();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _adService = null;
                    _seedService = null;
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
