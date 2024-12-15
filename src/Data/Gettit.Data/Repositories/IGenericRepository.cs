namespace Gettit.Data.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsNoTracking();

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
