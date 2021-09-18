using Persistance.Abstractions.Interfaces;
using QB.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Repositories
{
    public interface IStateRepository : IGenericRepositoryAsync<State>
    {
        //! The 'new' is intended to hide inherited GetAllAsync(); method.
        new Task<IEnumerable<State>> GetAllAsync();
    }
}
