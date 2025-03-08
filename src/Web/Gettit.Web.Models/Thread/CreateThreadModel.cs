using Gettit.Web.Models.Utilities.Binding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Models.Thread
{
    public class CreateThreadModel
    {
        public string CommunityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [BindProperty(BinderType = typeof(TagsModelBinder))]
        public List<string> Tags { get; set; }

        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
}