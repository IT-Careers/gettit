using System.Diagnostics;
using Gettit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult APiAction()
        {
            return Json(new List<object>([
               new { Name = "Pesho", Age = 15 },
               new { Name = "Pesho", Age = 15 },
               new { Name = "Pesho", Age = 15 },
               new { Name = "Pesho", Age = 15 },
            ]));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
