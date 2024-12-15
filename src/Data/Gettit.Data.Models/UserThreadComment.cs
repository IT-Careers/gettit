namespace Gettit.Data.Models
{
    public class UserThreadComment : BaseEntity
    {
        public GettitUser User { get; set; }

        public GettitThread Thread { get; set; }

        public Comment Comment { get; set; }
    }
}
