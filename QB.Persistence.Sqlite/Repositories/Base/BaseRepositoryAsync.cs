using Microsoft.EntityFrameworkCore;
using Persistance.Abstractions;

namespace QB.Persistence.Sqlite.Repositories.Base
{
    public abstract class BaseRepositoryAsync<TEntity>
        : GenericRepositoryAsync<TEntity> where TEntity : class
    {
        new protected readonly SqliteDbContext _context;

        protected BaseRepositoryAsync(DbContext context)
            : base(context)
        {
            _context = context as SqliteDbContext;
        }
    }
}
