using Gettit.Service.Community;
using Gettit.Service.Models;
using Gettit.Service.Thread;
using Gettit.Web.Models.Comment;
using Gettit.Web.Models.Community;
using Gettit.Web.Models.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Controllers
{
    public class ThreadController : Controller
    {
        private readonly IGettitCommunityService _gettitCommunityService;

        private readonly IGettitThreadService _gettitThreadService;

        public ThreadController(IGettitCommunityService gettitCommunityService, IGettitThreadService gettitThreadService)
        {
            _gettitCommunityService = gettitCommunityService;
            _gettitThreadService = gettitThreadService;
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
            await this._gettitThreadService.CreateAsync(new GettitThreadServiceModel
            {
                Title = createThreadModel.Title,
                Content = createThreadModel.Content,
                Tags = createThreadModel.Tags.Select(tag => new GettitTagServiceModel { Label = tag }).ToList(),
                Community = new GettitCommunityServiceModel
                {
                    Id = createThreadModel.CommunityId
                }
            });

            // TODO: Redirect to Thread Page
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string threadId)
        {
            GettitThreadServiceModel thread = await this._gettitThreadService.GetByIdAsync(threadId);

            if(thread == null)
            {
                return NotFound("Thread not found...");
            }

            return View(thread);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Comment(
            [FromBody] CreateCommentModel model,
            [FromQuery] string threadId,
            [FromQuery] string? parentId = null)
        {
            var result = await this._gettitThreadService.CreateCommentOnThread(new CommentServiceModel
            {
                Content = model.Content,
                //Attachments
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
    }
}
