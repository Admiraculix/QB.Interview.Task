using QB.Application.Dtos;
using QB.Application.Interfaces.Services.Business.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Services.Business
{
    public interface ICountryBusinessService : IBusinessService
    {
        Task<CountryDto> GetCountryByIdAsync(CountryDto request);
        Task<IEnumerable<CountryDto>> GetAllCountriesAsync();
        Task<IEnumerable<CountryPopulationDto>> GetAllPopulationOfCountriesAsync();
        Task<CountryDto> CreateCountryAsync(CountryDto request);
        Task<CountryDto> UpdateCountryAsync(CountryDto request);
        Task<CountryDto> DeleteCountryAsync(CountryDto request);
    }
}
