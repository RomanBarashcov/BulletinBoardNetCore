using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace AppleUsed.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ISeedService _seedService;
        private IAdService _adService;

        public HomeController(ISeedService seedService, IAdService adService)
        {
            _seedService = seedService;
            _adService = adService;
        }

        public async Task<IActionResult> Index()
        {
            var ads = await _adService.GetAds();
            var modelList = ads.OrderByDescending(x=>x.AdId).Take(5).ToList();
            return View(modelList);
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
