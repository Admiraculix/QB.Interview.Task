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
    public class CountryController : BaseController
    {
        private readonly ICountryBusinessService _countryBusinessService;

        public CountryController(ICountryBusinessService countryBusinessService)
        {
            _countryBusinessService = countryBusinessService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCountryByIdAsync([FromRoute] GetCountryByIdRequest request)
        {
            var dtoRequest = Mapper.Map<CountryDto>(request);
            var result = await _countryBusinessService.GetCountryByIdAsync(dtoRequest);
            var response = Mapper.Map<CountryResponse>(result);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _countryBusinessService.GetAllCountriesAsync();
            var response = Mapper.Map<IEnumerable<CountryResponse>>(result);

            return Ok(response);
        }

        [HttpGet("Populations")]
        public async Task<IActionResult> GetAllPopulationOfCountriesAsync()
        {
            var result = await _countryBusinessService.GetAllPopulationOfCountriesAsync();
            var response = Mapper.Map<IEnumerable<CountryPopulationResponse>>(result);

            return Ok(response);
        }
    }
}
