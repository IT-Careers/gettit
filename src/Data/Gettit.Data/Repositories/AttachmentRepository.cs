namespace Gettit.Data.Repositories
{
    using Gettit.Data.Models;
    using Gettit.Web.Data;

    public class AttachmentRepository : BaseGenericRepository<Attachment>
    {
        public AttachmentRepository(GettitDbContext dbContext) : base(dbContext)
        {
        }
    }
}
