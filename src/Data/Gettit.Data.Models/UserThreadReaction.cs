namespace Gettit.Data.Models
{
    public class UserThreadReaction : BaseEntity
    {
        public GettitUser User { get; set; }

        public GettitThread Thread { get; set; }

        public Reaction Reaction { get; set; }
    }
}
