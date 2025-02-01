using Gettit.Web.Models.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class ThreadController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Shared/ThreadCommunityCreate.cshtml");
        }

        [HttpPost]
        public IActionResult CreateConfirm(CreateThreadModel createThreadModel)
        {
            return View("~/Views/Shared/ThreadCommunityCreate.cshtml");
        }
    }
}
