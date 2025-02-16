using Gettit.Service.Cloud;
using Gettit.Service.Community;
using Gettit.Service.Models;
using Gettit.Service.Reaction;
using Gettit.Service.Thread;
using Gettit.Web.Models.Comment;
using Gettit.Web.Models.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IGettitCommunityService _gettitCommunityService;

        private readonly IGettitThreadService _gettitThreadService;

        private readonly IReactionService _reactionService;

        private readonly ICloudinaryService _cloudinaryService;

        public ThreadController(
            IGettitCommunityService gettitCommunityService,
            IGettitThreadService gettitThreadService,
            IReactionService reactionService,
            ICloudinaryService cloudinaryService)
        {
            _gettitCommunityService = gettitCommunityService;
            _gettitThreadService = gettitThreadService;
            _reactionService = reactionService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            this.ViewData["Communities"] = this._gettitCommunityService.GetAll().ToList();

            return View("~/Views/Shared/ThreadCommunityCreate.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirm(CreateThreadModel createThreadModel)
        {
            List<AttachmentServiceModel> threadAttachments = new List<AttachmentServiceModel>();

            foreach (var attachment in createThreadModel.Attachments)
            {
                threadAttachments.Add(new AttachmentServiceModel
                {
                    CloudUrl = await this.UploadFile(attachment)
                });
            }

            await this._gettitThreadService.CreateAsync(new GettitThreadServiceModel
            {
                Title = createThreadModel.Title,
                Content = createThreadModel.Content,
                Tags = createThreadModel.Tags.Select(tag => new GettitTagServiceModel { Label = tag }).ToList(),
                Community = new GettitCommunityServiceModel
                {
                    Id = createThreadModel.CommunityId
                },
                Attachments = threadAttachments
            });

            // TODO: Redirect to Thread Page
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string threadId)
        {
            GettitThreadServiceModel thread = await this._gettitThreadService.GetByIdAsync(threadId);

            this.ViewData["Reactions"] = this._reactionService.GetAll().ToList();

            if(thread == null)
            {
                return NotFound("Thread not found...");
            }

            return View(thread);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(
            [FromForm] CreateCommentModel model,
            [FromQuery] string threadId,
            [FromQuery] string? parentId = null)
        {

            List<AttachmentServiceModel> commentAttachments = new List<AttachmentServiceModel>();

            if (model.Attachments != null)
            {
                foreach (var attachment in model.Attachments)
                {
                    commentAttachments.Add(new AttachmentServiceModel
                    {
                        CloudUrl = await this.UploadFile(attachment)
                    });
                }
            }

            var result = await this._gettitThreadService.CreateCommentOnThread(new CommentServiceModel
            {
                Content = model.Content,
                Attachments = commentAttachments
            }, threadId, parentId);

            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> React(
            [FromQuery] string threadId,
            [FromQuery] string reactionId)
        {
            var result = await this._gettitThreadService.CreateReactionOnThread(threadId, reactionId);

            return Ok(result);
        }

        private async Task<string> UploadFile(IFormFile photo)
        {
            var uploadResponse = await this._cloudinaryService.UploadFile(photo);

            if (uploadResponse == null)
            {
                return null;
            }

            return uploadResponse["url"].ToString();
        }
    }
}
