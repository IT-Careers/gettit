using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gettit.Data.Repositories
{
    public abstract class MetadataBaseGenericRepository<TEntity> : BaseGenericRepository<TEntity> where TEntity : MetadataBaseEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected MetadataBaseGenericRepository(GettitDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public override async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = await this.GetUser();
            return await base.CreateAsync(entity);
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = await this.GetUser();
            return await base.UpdateAsync(entity);
        }

        public override async Task<TEntity> DeleteAsync(TEntity entity)
        {
            entity.DeletedOn = DateTime.UtcNow;
            entity.DeletedBy = await this.GetUser();
            return await base.DeleteAsync(entity);
        }

        private async Task<GettitUser> GetUser()
        {
            string? userId = this._httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await this._dbContext.Users.SingleOrDefaultAsync(user => user.Id == userId);
        }
    }
}
