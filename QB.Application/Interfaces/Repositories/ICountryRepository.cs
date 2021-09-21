using Persistance.Abstractions.Interfaces;
using QB.Domain.Models;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Repositories
{
    public interface ICountryRepository : IGenericRepositoryAsync<Country>
    {
        Task<Country> FindCountryByNameAsync(string name);
    }
}
