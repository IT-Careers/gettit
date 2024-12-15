using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Gettit.Data.Repositories
{
    public class ReactionRepository : MetadataBaseGenericRepository<Reaction>
    {
        public ReactionRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
