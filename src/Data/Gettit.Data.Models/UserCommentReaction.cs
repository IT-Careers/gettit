namespace Gettit.Data.Models
{
    public class UserCommentReaction : BaseEntity
    {
        public GettitUser User { get; set; }

        public Comment Comment { get; set; }

        public Reaction Reaction { get; set; }
    }
}
