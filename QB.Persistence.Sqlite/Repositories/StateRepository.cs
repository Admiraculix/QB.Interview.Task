using QB.Application.Interfaces.Repositories;
using QB.Domain.Models;
using QB.Persistence.Sqlite.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QB.Persistence.Sqlite.Repositories
{

    public class StateRepository : GenericRepositoryAsync<State>, IStateRepository
    {
        public StateRepository(SqliteDbContext context)
            : base(context)
        {

        }

        //! The 'new' is intended to hide inherited GetAllAsync(); method.
        new public async Task<IEnumerable<State>> GetAllAsync()
        {
            var result = await _context.State.AsAsyncEnumerable()
                            .OrderBy(x => x.StateId)
                            .Where(x => x.StateId > 0)
                            .ToListAsync();
            return result;
        }
    }
}
