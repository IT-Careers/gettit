using Microsoft.AspNetCore.Http;

namespace Gettit.Web.Models.Reaction
{
    public class CreateReactionModel
    {
        public string Label { get; set; }

        public IFormFile Reaction { get; set; }
    }
}
