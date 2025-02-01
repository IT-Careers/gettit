using Gettit.Service.Community;
using Gettit.Service.Models;
using Gettit.Web.Models.Community;
using Gettit.Web.Models.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly IGettitCommunityService gettitCommunityService;

        public CommunityController(IGettitCommunityService gettitCommunityService)
        {
            this.gettitCommunityService = gettitCommunityService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("~/Views/Shared/ThreadCommunityCreate.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirm(CreateCommunityModel createCommunityModel)
        {
            await this.gettitCommunityService.CreateAsync(new GettitCommunityServiceModel
            {
                Name = createCommunityModel.Name,
                Description = createCommunityModel.Description,
                Tags = createCommunityModel.Tags.Select(tag => new GettitTagServiceModel { Label = tag }).ToList()
                // TODO: photoes
            });

            // TODO: Redirect to Community Page
            return Redirect("/");
        }
    }
}
