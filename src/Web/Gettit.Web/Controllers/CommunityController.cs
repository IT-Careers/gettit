using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class CommunityController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
