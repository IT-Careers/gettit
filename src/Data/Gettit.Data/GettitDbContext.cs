using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Web.Data
{
    public class GettitDbContext : IdentityDbContext
    {
        public GettitDbContext(DbContextOptions<GettitDbContext> options)
            : base(options)
        {
        }
    }
}
