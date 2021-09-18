using QB.Application.Dtos;
using QB.Application.Interfaces.Services.Business.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Services.Business
{
    public interface ICityBusinessService : IBusinessService
    {
        Task<CityDto> GetCityByIdAsync(CityDto request);
        Task<IEnumerable<CityDto>> GetAllByStateIdAsync(CityDto request);
    }
}
