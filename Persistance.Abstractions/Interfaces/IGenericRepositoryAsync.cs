using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Abstractions.Interfaces
{
    public interface IGenericRepositoryAsync<T>
        where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        IQueryable<T> GetAllAsQuerable();
    }
}
