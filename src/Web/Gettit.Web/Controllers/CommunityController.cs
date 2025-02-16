using Gettit.Service.Cloud;
using Gettit.Service.Community;
using Gettit.Service.Models;
using Gettit.Service.Reaction;
using Gettit.Service.Thread;
using Gettit.Web.Models.Community;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly IGettitCommunityService gettitCommunityService;

        private readonly IGettitThreadService gettitThreadService;

        private readonly IReactionService reactionService;

        private readonly ICloudinaryService cloudinaryService;

        public CommunityController(
            IGettitCommunityService gettitCommunityService,
            ICloudinaryService cloudinaryService,
            IGettitThreadService gettitThreadService,
            IReactionService reactionService)
        {
            this.gettitCommunityService = gettitCommunityService;
            this.cloudinaryService = cloudinaryService;
            this.gettitThreadService = gettitThreadService;
            this.reactionService = reactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("~/Views/Shared/ThreadCommunityCreate.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirm(CreateCommunityModel createCommunityModel)
        {
            var thumbnailPhotoUrl = await this.UploadPhoto(createCommunityModel.ThumbnailPhoto);
            var bannerPhotoUrl = await this.UploadPhoto(createCommunityModel.BannerPhoto);

            await this.gettitCommunityService.CreateAsync(new GettitCommunityServiceModel
            {
                Name = createCommunityModel.Name,
                Description = createCommunityModel.Description,
                Tags = createCommunityModel.Tags.Select(tag => new GettitTagServiceModel { Label = tag }).ToList(),
                ThumbnailPhoto = new AttachmentServiceModel { CloudUrl = thumbnailPhotoUrl },
                BannerPhoto = new AttachmentServiceModel { CloudUrl = bannerPhotoUrl }
            });

            // TODO: Redirect to Community Page
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string communityId)
        {
            this.ViewData["Threads"] = this.gettitThreadService.GetAllByCommunityId(communityId).ToList();
            this.ViewData["Reactions"] = this.reactionService.GetAll().ToList();

            return View(await this.gettitCommunityService.GetByIdAsync(communityId));
        }
            
        private async Task<string> UploadPhoto(IFormFile photo)
        {
            var uploadResponse = await this.cloudinaryService.UploadFile(photo);

            if (uploadResponse == null)
            {
                return null;
            }

            return uploadResponse["url"].ToString();
        }
    }
}
