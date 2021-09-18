using AutoMapper;
using QB.Application.Dtos;
using QB.Domain.Models;

namespace QB.Application.AutoMapper.Profiles
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<CityDto, City>();
            CreateMap<StateDto, State>();
            CreateMap<CountryDto, Country>();
        }
    }
}
