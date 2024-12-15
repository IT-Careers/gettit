using Gettit.Data.Models;
using Gettit.Web.Data;

namespace Gettit.Data.Repositories
{
    public class GettitRoleRepository : BaseGenericRepository<GettitRole>
    {
        public GettitRoleRepository(GettitDbContext dbContext) : base(dbContext)
        {
        }
    }
}
