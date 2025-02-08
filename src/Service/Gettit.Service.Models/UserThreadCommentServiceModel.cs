namespace Gettit.Service.Models
{
    public class UserThreadCommentServiceModel : BaseServiceModel
    {
        public GettitUserServiceModel User { get; set; }

        public GettitThreadServiceModel Thread { get; set; }

        public CommentServiceModel Comment { get; set; }
    }
}
