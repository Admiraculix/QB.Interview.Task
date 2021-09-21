using AutoMapper;
using QB.API.Models.Requests;
using QB.Application.Dtos;

namespace QB.API.AutoMapper.Profiles
{
    public class RequestToDtoProfile : Profile
    {
        public RequestToDtoProfile()
        {
            MapCityModels();

            MapStateModels();

            MapCountryModels();
        }

        private void MapCountryModels()
        {
            CreateMap<GetCountryByIdRequest, CountryDto>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CountryRequest, CountryDto>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(s => s.Name))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void MapStateModels()
        {
            CreateMap<GetStateByIdRequest, StateDto>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<StateRequest, StateDto>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(s => s.Name))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(s => s.CountryId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void MapCityModels()
        {
            CreateMap<GetCityByIdRequest, CityDto>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<GetCityByStateIdRequest, CityDto>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
