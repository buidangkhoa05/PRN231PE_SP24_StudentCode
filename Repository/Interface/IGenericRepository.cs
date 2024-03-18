using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T?> Get(int id);

        Task Create(T t);

        Task Update(T t);

        Task Delete(int id);

        Task Delete(T entity);
    }
}
