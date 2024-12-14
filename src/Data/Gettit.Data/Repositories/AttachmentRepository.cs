using Gettit.Data.Models;
using Gettit.Web.Data;

namespace Gettit.Data.Repositories
{
    public class AttachmentRepository : BaseGenericRepository<Attachment>
    {
        public AttachmentRepository(GettitDbContext dbContext) : base(dbContext)
        {
        }
    }
}
