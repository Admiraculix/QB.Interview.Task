using AutoMapper;
using QB.Application.Configurations;
using QB.Application.Dtos;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Services.Utility;
using QB.Application.Services.Business.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Services.Utility
{
    public class NormalizeCountryNameService : BusinessService, INormalizeCountryNameService
    {
        private readonly IStatExternalService _statExternalService;
        private readonly CountryNameVariationConfiguration _countryNameVariationConfiguration;

        public NormalizeCountryNameService(
                IUnitOfWork unitOfWork,
                IMapper mapper,
                IStatExternalService statExternalService,
                CountryNameVariationConfiguration countryNameVariationConfiguration)
            : base(unitOfWork, mapper)
        {
            _statExternalService = statExternalService;
            _countryNameVariationConfiguration = countryNameVariationConfiguration;
        }

        public async Task<IEnumerable<CountryPopulationDto>> NormalizeUsaCountryNameAsync()
        {
            var countryPopulationExternalData = await _statExternalService.GetCountryPopulationsAsync();
            var externalCountryPopulationDtoList = _mapper.Map<IEnumerable<CountryPopulationDto>>(countryPopulationExternalData);

            foreach (var country in externalCountryPopulationDtoList)
            {
                foreach (var name in _countryNameVariationConfiguration.UsaNames)
                {
                    if (country.CountryName == name)
                    {
                        var usaCountry = await _unitOfWork.Countries.GetAsync(1);
                        country.CountryName = usaCountry.CountryName;
                    }
                }
            }

            return externalCountryPopulationDtoList;
        }
    }
}
