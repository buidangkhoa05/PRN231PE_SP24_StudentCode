using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAO
{
    public class GenericDAO<TEntity> where TEntity : class
    {
        private static GenericDAO<TEntity> instance = null;
        public static GenericDAO<TEntity> Instance => instance ??= new GenericDAO<TEntity>();

        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericDAO()
        {
            _dbContext = new SilverJewelry2024DbContext();
            _dbSet = _dbContext.Set<TEntity>();

        }

        public async Task<int> SaveChangeAsync() => await _dbContext.SaveChangesAsync();

        public virtual async Task<TEntity?> FindAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public virtual async Task<List<TEntity>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate)
                                .FirstOrDefaultAsync()
                                .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate)
                        .ToListAsync()
                        .ConfigureAwait(false);
        }


        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task CreateAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int entityID)
        {
            var entity = await FindAsync(entityID);

            if (entity == null)
                throw new Exception("Entity not found");

            _dbSet.RemoveRange(entity);
            await Task.CompletedTask;
        }
    }

}
