using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels;

namespace AppleUsed.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        //private readonly IBookBLL _bookBLL;

        //public HomeController(IBookBLL bookBLL)
        //{
        //    _bookBLL = bookBLL;
        //}

        public IActionResult Index()
        {
            return View();
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
