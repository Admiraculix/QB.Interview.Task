using Microsoft.EntityFrameworkCore;
using Persistance.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Abstractions
{
    public abstract class GenericRepositoryAsync<TEntity>
        : IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        protected GenericRepositoryAsync(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IAsyncEnumerable<TEntity> asyncEnumerable = _context.Set<TEntity>().AsAsyncEnumerable();
            return await asyncEnumerable.ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQuerable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            return Task.CompletedTask;
        }
    }
}
