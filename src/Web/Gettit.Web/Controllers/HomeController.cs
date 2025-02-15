using System.Diagnostics;
using Gettit.Service.Reaction;
using Gettit.Service.Thread;
using Gettit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGettitThreadService _gettitThreadService;

        private readonly IReactionService _reactionService;

        public HomeController(IGettitThreadService gettitThreadService, IReactionService reactionService)
        {
            _gettitThreadService = gettitThreadService;
            _reactionService = reactionService;
        }

        public IActionResult Index()
        {
            this.ViewData["Threads"] = this._gettitThreadService.GetAll().ToList();
            this.ViewData["Reactions"] = this._reactionService.GetAll().ToList();
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
