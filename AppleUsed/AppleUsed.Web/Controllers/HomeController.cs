using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using AppleUsed.BLL.DTO;

namespace AppleUsed.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ISeedService _seedService;
        private readonly IAdService _adService;

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
            List<AdDTO> model = new List<AdDTO>();

            var getAdsResult = await _adService.GetActiveAds();
            if(!getAdsResult.Succedeed)
                return View(model);

            model = getAdsResult.Property.OrderByDescending(x=>x.AdId).Take(5).ToList();
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
    }
}
