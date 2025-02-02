using System.Diagnostics;
using Gettit.Service.Thread;
using Gettit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGettitThreadService _gettitThreadService;

        public HomeController(IGettitThreadService gettitThreadService)
        {
            _gettitThreadService = gettitThreadService;
        }

        public IActionResult Index()
        {
            this.ViewData["Threads"] = this._gettitThreadService.GetAll().ToList();
            return View();
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
