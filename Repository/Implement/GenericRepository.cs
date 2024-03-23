using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public virtual async Task<List<T>> GetAll() => await GenericDAO<T>.Instance.FindAllAsync();

        public virtual async Task<T?> Get(int id) => await GenericDAO<T>.Instance.FindAsync(id);

        public virtual async Task Create(T t)
        {
            await GenericDAO<T>.Instance.CreateAsync(t);
            await GenericDAO<T>.Instance.SaveChangeAsync();
        }

        public  virtual async Task Update(T t)
        {
            await GenericDAO<T>.Instance.UpdateAsync(t);
            await GenericDAO<T>.Instance.SaveChangeAsync();
        }

        public virtual async Task Delete(int id)
        {
            await GenericDAO<T>.Instance.DeleteAsync(id);
            await GenericDAO<T>.Instance.SaveChangeAsync();
        }

        public virtual async Task Delete(T entity)
        {
            await GenericDAO<T>.Instance.DeleteAsync(entity);
            await GenericDAO<T>.Instance.SaveChangeAsync();
        }
    }
}
