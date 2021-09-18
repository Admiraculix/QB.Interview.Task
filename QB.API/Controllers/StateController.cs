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
    }
}
