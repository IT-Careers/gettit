using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Gettit.Data.Repositories
{
    public class GettitRoleRepository : MetadataBaseGenericRepository<GettitRole>
    {
        public GettitRoleRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
