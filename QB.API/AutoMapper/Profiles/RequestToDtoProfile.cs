using AutoMapper;
using QB.API.Models.Requests;
using QB.Application.Dtos;

namespace QB.API.AutoMapper.Profiles
{
    public class RequestToDtoProfile : Profile
    {
        public RequestToDtoProfile()
        {
            CreateMap<GetCityByIdRequest, CityDto>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<GetCityByStateIdRequest, CityDto>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<GetStateByIdRequest, StateDto>()
                 .ForMember(dest => dest.StateId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<GetCountryByIdRequest, CountryDto>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
