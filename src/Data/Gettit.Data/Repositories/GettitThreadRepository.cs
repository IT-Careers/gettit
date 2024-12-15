using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Gettit.Data.Repositories
{
    public class GettitThreadRepository : MetadataBaseGenericRepository<GettitThread>
    {
        public GettitThreadRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
