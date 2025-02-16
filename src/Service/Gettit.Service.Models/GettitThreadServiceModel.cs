namespace Gettit.Service.Models
{
    public class GettitThreadServiceModel : MetadataBaseServiceModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public List<AttachmentServiceModel> Attachments { get; set; }

        public GettitCommunityServiceModel Community { get; set; }

        public List<GettitTagServiceModel> Tags { get; set; }

        public List<UserThreadReactionServiceModel> Reactions { get; set; }

        public List<UserThreadCommentServiceModel> Comments { get; set; }
    }
}
