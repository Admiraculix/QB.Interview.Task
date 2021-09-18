using AutoMapper;
using QB.Application.Dtos;
using QB.Domain.Models;

namespace QB.Application.AutoMapper.Profiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<State, StateDto>();
            CreateMap<Country, CountryDto>();
        }
    }
}
