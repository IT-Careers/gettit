using Gettit.Web.Models.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class ThreadController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateConfirm(CreateThreadModel createThreadModel)
        {
            return View();
        }
    }
}
