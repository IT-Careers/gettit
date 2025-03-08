namespace Gettit.Service.Models
{
    public class UserCommentReactionServiceModel : BaseServiceModel
    {
        public GettitUserServiceModel User { get; set; }

        public CommentServiceModel Comment { get; set; }

        public ReactionServiceModel Reaction { get; set; }

        public bool IsDeleted { get; set; }
    }
}
