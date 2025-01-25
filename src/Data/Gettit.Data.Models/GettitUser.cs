using Microsoft.AspNetCore.Identity;

namespace Gettit.Data.Models
{
    public class GettitUser : IdentityUser
    {
        public GettitRole? ForumRole { get; set; }
    }
}
