using QB.Application.Interfaces.Repositories;
using QB.Domain.Models;
using QB.Persistence.Sqlite.Repositories.Base;

namespace QB.Persistence.Sqlite.Repositories
{
    public class CountryRepository : GenericRepositoryAsync<Country>, ICountryRepository
    {
        public CountryRepository(SqliteDbContext context)
            : base(context)
        {

        }
    }
}