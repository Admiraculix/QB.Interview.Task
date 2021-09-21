using Microsoft.AspNetCore.Mvc;
using QB.API.Controllers.Base;
using QB.API.Models.Requests;
using QB.API.Models.Responses;
using QB.Application.Dtos;
using QB.Application.Interfaces.Services.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.API.Controllers
{
    public class StateController : BaseController
    {
        private readonly IStateBusinessService _stateBusinessService;

        public StateController(IStateBusinessService stateBusinessService)
        {
            _stateBusinessService = stateBusinessService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStateByIdAsync([FromRoute] GetStateByIdRequest request)
        {
            var dtoRequest = Mapper.Map<StateDto>(request);
            var result = await _stateBusinessService.GetStateByIdAsync(dtoRequest);
            var response = Mapper.Map<StateResponse>(result);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _stateBusinessService.GetAllStatesAsync();
            var response = Mapper.Map<IEnumerable<StateResponse>>(result);

            return Ok(response);
        }
        /// <summary>
        /// NOT WORK!!!
        /// Adds the new state asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> AddNewStateAsync([FromBody] StateRequest request)
        {
            var dtoRequest = Mapper.Map<StateDto>(request);
            var result = await _stateBusinessService.CreateStateAsync(dtoRequest);
            var response = Mapper.Map<StateResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// NOT WORK!!!
        /// Updates the state name asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("{id}/Name")]
        public async Task<IActionResult> UpdateStateNameAsync([FromRoute] int id, [FromBody] StateRequest request)
        {
            request.Id = id;
            var dtoRequest = Mapper.Map<StateDto>(request);
            var result = await _stateBusinessService.UpdateStateAsync(dtoRequest);
            var response = Mapper.Map<StateResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// NOT WORK!!!
        /// Deletes the state by identifier asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStateByIdAsync([FromRoute] GetStateByIdRequest request)
        {
            var dtoRequest = Mapper.Map<StateDto>(request);
            var result = await _stateBusinessService.DeleteStateAsync(dtoRequest);
            var response = Mapper.Map<StateResponse>(result);

            return Ok(response);
        }
    }
}
