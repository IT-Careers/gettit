namespace Gettit.Data.Repositories
{
    using Gettit.Data.Models;
    using Gettit.Web.Data;
    using Microsoft.AspNetCore.Http;

    public class CategoryRepository : MetadataBaseGenericRepository<Category>
    {
        public CategoryRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
