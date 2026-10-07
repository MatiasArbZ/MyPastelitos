using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyPastelitos.Web.Models;
using MyPastelitos.Web.Services.Abstractions;
using System.Diagnostics;

namespace MyPastelitos.Web.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
