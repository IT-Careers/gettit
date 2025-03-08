using Gettit.Data.Models;
using Gettit.Web.Data;

namespace Gettit.Data.Repositories
{
    public class UserCommentReactionRepository : BaseGenericRepository<UserCommentReaction>
    {
        public UserCommentReactionRepository(GettitDbContext dbContext) : base(dbContext)
        {
        }
    }
}
