namespace FS.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity item);
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(Func<TEntity, ValueTask<bool>> predicate);
        Task<TEntity?> GetById(int id);
        Task<TEntity> Update(TEntity item);
        Task Delete(TEntity item);
    }
}
