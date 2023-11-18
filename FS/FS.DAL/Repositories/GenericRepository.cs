using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FS.DAL.Repositories
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(DbContext context)
        {
            this._context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task Delete(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task<TEntity> Add(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
