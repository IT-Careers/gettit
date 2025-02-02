using Gettit.Service;
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

        private readonly ICloudinaryService cloudinaryService;

        public CommunityController(IGettitCommunityService gettitCommunityService, 
            ICloudinaryService cloudinaryService)
        {
            this.gettitCommunityService = gettitCommunityService;
            this.cloudinaryService = cloudinaryService;
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
