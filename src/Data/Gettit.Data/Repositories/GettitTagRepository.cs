using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Gettit.Data.Repositories
{
    public class GettitTagRepository : MetadataBaseGenericRepository<GettitTag>
    {
        public GettitTagRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
