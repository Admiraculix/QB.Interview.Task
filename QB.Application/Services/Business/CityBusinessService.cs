using AutoMapper;
using QB.Application.Dtos;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Services.Business;
using QB.Application.Services.Business.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Services.Business
{
    public class CityBusinessService : BusinessService, ICityBusinessService
    {
        public CityBusinessService(
            IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<CityDto> GetCityByIdAsync(CityDto request)
        {
            var city = await _unitOfWork.Cities.GetAsync(request.CityId);
            var cityDto = _mapper.Map<CityDto>(city);

            return cityDto;
        }

        public async Task<IEnumerable<CityDto>> GetAllByStateIdAsync(CityDto request)
        {
            var cities = await _unitOfWork.Cities.GetAllAsync(request.StateId);
            var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);

            return cityDtos;
        }
    }
}
