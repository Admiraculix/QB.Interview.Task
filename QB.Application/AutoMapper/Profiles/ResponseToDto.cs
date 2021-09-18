﻿using AutoMapper;
using QB.Application.Dtos;
using System;

namespace QB.Application.AutoMapper.Profiles
{
    public class ResponseToDto : Profile
    {
        public ResponseToDto()
        {
            CreateMap<Tuple<string, int>, CountryPopulationDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(s => s.Item1))
                .ForMember(dest => dest.Population, opt => opt.MapFrom(s => s.Item2));
        }
    }
}
