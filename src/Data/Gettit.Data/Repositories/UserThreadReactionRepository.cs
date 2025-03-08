using Gettit.Data.Models;
using Gettit.Web.Data;

namespace Gettit.Data.Repositories
{
    public class UserThreadReactionRepository : BaseGenericRepository<UserThreadReaction>
    {
        public UserThreadReactionRepository(GettitDbContext dbContext) : base(dbContext)
        {
        }
    }
}
