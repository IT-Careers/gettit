namespace Gettit.Data.Models
{
    public class Comment : MetadataBaseEntity
    {
        public string Content { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<UserCommentReaction> Reactions { get; set; }

        public List<Comment> Replies { get; set; }

        public Comment? Parent { get; set; }
    }
}
