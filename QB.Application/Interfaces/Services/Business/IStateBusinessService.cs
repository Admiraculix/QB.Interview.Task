using QB.Application.Dtos;
using QB.Application.Interfaces.Services.Business.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Services.Business
{
    public interface IStateBusinessService : IBusinessService
    {
        Task<StateDto> GetStateByIdAsync(StateDto request);
        Task<IEnumerable<StateDto>> GetAllStatesAsync();
        Task<StateDto> CreateStateAsync(StateDto request);
        Task<StateDto> UpdateStateAsync(StateDto request);
        Task<StateDto> DeleteStateAsync(StateDto request);
    }
}
