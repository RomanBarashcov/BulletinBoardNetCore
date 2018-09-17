using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class AdministrationAdController : Controller
    {
        private IAdService _adService { get; set; }

        public AdministrationAdController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            AdIndexViewModel model = new AdIndexViewModel { SimpleAds = new List<AdDTO>() };

            var result = await _adService.GetActiveAds();
            if (!result.Succedeed)
                return View(model);

            int count = model.SimpleAds.Count();
            model.PageViewModel = new PageViewModel(count, page, pageSize);
            model.SimpleAds = model.SimpleAds.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }
    }
}