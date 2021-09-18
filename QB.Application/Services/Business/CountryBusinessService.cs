using AutoMapper;
using EqualityComparer.Abstractions;
using Microsoft.EntityFrameworkCore;
using QB.Application.Dtos;
using QB.Application.Extensions;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Services.Business;
using QB.Application.Interfaces.Services.Utility;
using QB.Application.Services.Business.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QB.Application.Services.Business
{
    public class CountryBusinessService : BusinessService, ICountryBusinessService
    {
        private readonly INormalizeCountryNameService _normalizeCountryNameService;

        public CountryBusinessService(
            IUnitOfWork unitOfWork, IMapper mapper,
            INormalizeCountryNameService normalizeCountryNameService)
            : base(unitOfWork, mapper)
        {
            _normalizeCountryNameService = normalizeCountryNameService;
        }
        public async Task<CountryDto> GetCountryByIdAsync(CountryDto request)
        {
            var country = await _unitOfWork.Countries.GetAsync(request.CountryId);
            var countryDto = _mapper.Map<CountryDto>(country);

            return countryDto;
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _unitOfWork.Countries.GetAllAsync();
            var countryDtos = _mapper.Map<IEnumerable<CountryDto>>(countries);

            return countryDtos;
        }

        public async Task<IEnumerable<CountryPopulationDto>> GetAllPopulationOfCountriesAsync()
        {
            var normalizeExternalCountryPopulationDtoList = await _normalizeCountryNameService.NormalizeUsaCountryNameAsync();
            var countryEntityList = await _unitOfWork.Countries.GetAllAsQuerable().ToListAsync();
            var stateEntityList = await _unitOfWork.States.GetAllAsQuerable().ToListAsync();
            var cityEntityList = await _unitOfWork.Cities.GetAllAsync();

            var statePopulationDtoList =
                      cityEntityList.OrderBy(st => st.StateId).ToList()
                     .GroupBy(c => c.StateId)
                     .Select(g => new StatePopulationDto
                     {
                         StateId = g.Key,
                         StateName = stateEntityList.SingleOrDefault(x => x.StateId == g.Key).StateName,
                         Population = g.Sum(s => s.Population),
                     }).ToList().AddCountryIds(stateEntityList);

            var databaseCountryPopulationDtoList =
                statePopulationDtoList.OrderBy(ct => ct.StateId).ToList()
                    .GroupBy(c => c.CountryId)
                        .Select(g => new CountryPopulationDto
                        {
                            CountryId = g.Key,
                            CountryName = countryEntityList.SingleOrDefault(x => x.CountryId == g.Key).CountryName,
                            Population = g.Sum(s => s.Population),
                        }).ToList();

            var comparer = new InlineComparer<CountryPopulationDto>(
                    (dtoFirst, dtoSecond) => dtoFirst.CountryName == dtoSecond.CountryName,
                    dto => dto.CountryName.GetHashCode());

            var unionListDtos = databaseCountryPopulationDtoList.Union(normalizeExternalCountryPopulationDtoList, comparer).ToList();

            return unionListDtos;
        }
    }
}
