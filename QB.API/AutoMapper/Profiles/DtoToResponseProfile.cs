using AutoMapper;
using QB.API.Models.Responses;
using QB.Application.Dtos;

namespace QB.API.AutoMapper.Profiles
{
    public class DtoToResponseProfile : Profile
    {
        public DtoToResponseProfile()
        {
            CreateMap<CityDto, CityResponse>();
            CreateMap<StateDto, StateResponse>();
            CreateMap<CountryDto, CountryResponse>();
            CreateMap<CountryPopulationDto, CountryPopulationResponse>();
        }
    }
}
