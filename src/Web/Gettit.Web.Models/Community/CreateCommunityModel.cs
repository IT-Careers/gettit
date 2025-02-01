using Gettit.Web.Models.Utilities.Binding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gettit.Web.Models.Community
{
    public class CreateCommunityModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [BindProperty(BinderType = typeof(TagsModelBinder))]
        public List<string> Tags { get; set; }

        public IFormFile ThumbnailPhoto { get; set; }

        public IFormFile BannerPhoto { get; set; }
    }
}
