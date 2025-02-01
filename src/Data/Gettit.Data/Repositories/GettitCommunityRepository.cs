namespace Gettit.Data.Repositories
{
    using Gettit.Data.Models;
    using Gettit.Web.Data;
    using Microsoft.AspNetCore.Http;

    public class GettitCommunityRepository : MetadataBaseGenericRepository<GettitCommunity>
    {
        public GettitCommunityRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
