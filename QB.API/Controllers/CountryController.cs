using Microsoft.AspNetCore.Mvc;
using QB.API.Controllers.Base;
using QB.API.Models.Requests;
using QB.API.Models.Responses;
using QB.Application.Dtos;
using QB.Application.Interfaces.Services.Business;
using System;
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

        [HttpPost()]
        public async Task<IActionResult> AddNewCountryAsync([FromBody] CountryRequest request)
        {
            var dtoRequest = Mapper.Map<CountryDto>(request);
            var result = await _countryBusinessService.CreateCountryAsync(dtoRequest);
            var response = Mapper.Map<CountryResponse>(result);

            return Ok(response);
        }

        [HttpPut("{id}/Name")]
        public async Task<IActionResult> UpdateCountryNameAsync([FromRoute] int id, [FromBody] CountryRequest request)
        {
            request.Id = id;
            var dtoRequest = Mapper.Map<CountryDto>(request);
            var result = await _countryBusinessService.UpdateCountryAsync(dtoRequest);
            var response = Mapper.Map<CountryResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// NOT WORK!!!
        /// Deletes the country by identifier asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCountryByIdAsync([FromRoute] GetCountryByIdRequest request)
        {
            var dtoRequest = Mapper.Map<CountryDto>(request);
            var result = await _countryBusinessService.DeleteCountryAsync(dtoRequest);
            var response = Mapper.Map<CountryResponse>(result);

            return Ok(response);
        }
    }
}
