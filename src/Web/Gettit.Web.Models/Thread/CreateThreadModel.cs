namespace Gettit.Web.Models.Thread
{
    public class CreateThreadModel
    {
        public string CommunityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<string> Tags { get; set; }
    }
}