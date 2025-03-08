namespace Gettit.Service.Models
{
    public class UserThreadReactionServiceModel : BaseServiceModel
    {
        public GettitUserServiceModel User { get; set; }

        public GettitThreadServiceModel Thread { get; set; }

        public ReactionServiceModel Reaction { get; set; }

        public bool IsDeleted { get; set; }
    }
}
