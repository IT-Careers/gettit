using Microsoft.AspNetCore.Http;

namespace Gettit.Web.Models.Comment
{
    public class CreateCommentModel
    {
        public string Content { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
