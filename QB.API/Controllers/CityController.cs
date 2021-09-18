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
    public class CityController : BaseController
    {
        private readonly ICityBusinessService _cityBusinessService;

        public CityController(ICityBusinessService cityBusinessService)
        {
            _cityBusinessService = cityBusinessService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCityByIdAsync([FromRoute] GetCityByIdRequest request)
        {
            var dtoRequest = Mapper.Map<CityDto>(request);
            var result = await _cityBusinessService.GetCityByIdAsync(dtoRequest);
            var response = Mapper.Map<CityResponse>(result);

            return Ok(response);
        }

        [HttpGet("State/{Id}")]
        public async Task<IActionResult> GetAllByStateIdAsync([FromRoute] GetCityByStateIdRequest request)
        {
            var dtoRequest = Mapper.Map<CityDto>(request);
            var result = await _cityBusinessService.GetAllByStateIdAsync(dtoRequest);
            var response = Mapper.Map<IEnumerable<CityResponse>>(result);

            return Ok(response);
        }
    }
}
