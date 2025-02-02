namespace Gettit.Service
{
    public interface IGenericService<TEntity, TModel>
    {
        IQueryable<TModel> GetAll();

        Task<TModel> GetByIdAsync(string id);

        Task<TEntity> InternalGetByIdAsync(string id);

        Task<TModel> CreateAsync(TModel model);

        Task<TEntity> InternalCreateAsync(TEntity model);

        Task<TModel> UpdateAsync(string id, TModel model);

        Task<TModel> DeleteAsync(string id);
    }
}
