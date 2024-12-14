using Gettit.Data.Models;
using Gettit.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Data.Repositories
{
    public abstract class BaseGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly GettitDbContext _dbContext;

        protected BaseGenericRepository(GettitDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await this._dbContext.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            this._dbContext.Remove(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> EditAsync(TEntity entity)
        {
            this._dbContext.Update(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._dbContext.Set<TEntity>().AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return this._dbContext.Set<TEntity>().AsNoTracking();
        }
    }
}
