using Persistance.Abstractions.Interfaces;
using QB.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Repositories
{
    public interface ICityRepository : IGenericRepositoryAsync<City>
    {
        Task<IEnumerable<City>> GetAllAsync(int stateId);
    }
}
