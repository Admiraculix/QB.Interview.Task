using AutoMapper;
using QB.Application.Dtos;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Services.Business;
using QB.Application.Services.Business.Base;
using QB.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Services.Business
{
    public class StateBusinessService : BusinessService, IStateBusinessService
    {
        public StateBusinessService(
            IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
        public async Task<StateDto> GetStateByIdAsync(StateDto request)
        {
            var state = await _unitOfWork.States.GetAsync(request.StateId);
            var stateDto = _mapper.Map<StateDto>(state);

            return stateDto;
        }

        public async Task<IEnumerable<StateDto>> GetAllStatesAsync()
        {
            var states = await _unitOfWork.States.GetAllAsync();
            var stateDtos = _mapper.Map<IEnumerable<StateDto>>(states);

            return stateDtos;
        }

        public async Task<StateDto> CreateStateAsync(StateDto request)
        {
            var entity = _mapper.Map<State>(request);
            await _unitOfWork.States.AddAsync(entity);
            var result = _unitOfWork.Commit();

            var state = await _unitOfWork.States.GetAsync(request.CountryId);
            var stateDto = _mapper.Map<StateDto>(state);

            return stateDto;
        }

        public async Task<StateDto> UpdateStateAsync(StateDto request)
        {
            var entity = _mapper.Map<State>(request);
            await _unitOfWork.States.UpdateAsync(entity);
            var result = _unitOfWork.Commit();

            var state = await _unitOfWork.States.GetAsync(request.CountryId);
            var stateDto = _mapper.Map<StateDto>(state);

            return stateDto;
        }

        public async Task<StateDto> DeleteStateAsync(StateDto request)
        {
            var entity = _mapper.Map<State>(request);
            await _unitOfWork.States.DeleteAsync(entity);
            var result = _unitOfWork.Commit();

            return request;
        }
    }
}
