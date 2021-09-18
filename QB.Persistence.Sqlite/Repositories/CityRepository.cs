using QB.Application.Interfaces.Repositories;
using QB.Domain.Models;
using QB.Persistence.Sqlite.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QB.Persistence.Sqlite.Repositories
{
    public class CityRepository : GenericRepositoryAsync<City>, ICityRepository
    {
        public CityRepository(SqliteDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetAllAsync(int stateId)
        {
            var cities = _context.City.AsAsyncEnumerable().Where(x => x.StateId == stateId);
            var result = await cities.ToListAsync();
            return result;
        }
    }
}
