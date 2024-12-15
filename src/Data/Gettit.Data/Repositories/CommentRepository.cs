using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Gettit.Data.Repositories
{
    public class CommentRepository : MetadataBaseGenericRepository<Comment>
    {
        public CommentRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor)
        {
        }
    }
}
