using Microsoft.AspNetCore.Identity;

namespace Gettit.Service.Models
{
    public class GettitUserServiceModel : IdentityUser
    {
        public GettitRoleServiceModel ForumRole { get; set; }
    }
}
