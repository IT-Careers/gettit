namespace Gettit.Service.Models
{
    public class CommentServiceModel : MetadataBaseServiceModel
    {
        public string Content { get; set; }

        public List<AttachmentServiceModel> Attachments { get; set; }

        public List<UserCommentReactionServiceModel> Reactions { get; set; }

        public List<CommentServiceModel> Replies { get; set; }

        public CommentServiceModel? Parent { get; set; }
    }
}
