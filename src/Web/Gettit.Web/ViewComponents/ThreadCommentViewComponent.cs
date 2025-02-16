using Gettit.Service.Comment;
using Gettit.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Web.ViewComponents
{

    [ViewComponent]
    public class ThreadCommentViewComponent : ViewComponent
    {
        private readonly ICommentService commentService;

        public ThreadCommentViewComponent(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? threadId = null, string? parentId = null, List<ReactionServiceModel> reactions = null)
        {
            this.ViewData["Reactions"] = reactions ?? new List<ReactionServiceModel>();

            if (threadId != null)
            {
                var threadComments = (await this.commentService.GetAllByThreadId(threadId)).ToList();
                return View(threadComments);
            }

            if (parentId != null)
            {
                var parentComments = await this.commentService.GetAllByParentId(parentId).ToListAsync();
                return View(parentComments);
            }

            return View(new List<CommentServiceModel>());
        }
    }
}
