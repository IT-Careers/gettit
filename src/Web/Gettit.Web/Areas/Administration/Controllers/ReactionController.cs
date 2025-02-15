using Gettit.Service.Cloud;
using Gettit.Service.Community;
using Gettit.Service.Models;
using Gettit.Service.Reaction;
using Gettit.Web.Models.Reaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class ReactionController : Controller
    {
        private readonly IReactionService reactionService;
        
        private readonly ICloudinaryService cloudinaryService;

        public ReactionController(IReactionService reactionService, ICloudinaryService cloudinaryService)
        {
            this.reactionService = reactionService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            return View(this.reactionService.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirm(CreateReactionModel model)
        {
            var reactionEmote = await this.UploadPhoto(model.Reaction);

            await reactionService.CreateAsync(new ReactionServiceModel
            {
                Label = model.Label,
                Emote = new AttachmentServiceModel { CloudUrl = reactionEmote }
            });

            return Redirect("/Administration/Reaction");
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
