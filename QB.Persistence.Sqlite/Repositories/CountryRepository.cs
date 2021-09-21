using QB.Application.Interfaces.Repositories;
using QB.Domain.Models;
using QB.Persistence.Sqlite.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;

namespace QB.Persistence.Sqlite.Repositories
{
    public class CountryRepository : BaseRepositoryAsync<Country>, ICountryRepository
    {
        public CountryRepository(SqliteDbContext context)
            : base(context)
        {

        }

        public async Task<Country> FindCountryByNameAsync(string name)
        {
            var result = await _context.Country.AsAsyncEnumerable()
                .SingleOrDefaultAsync(x => x.CountryName == name);
            return result;
        }
    }
}