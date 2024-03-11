using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Models;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Journey, JourneyDto>().ReverseMap();
            CreateMap<JourneyDto, Journey>().ReverseMap();
            CreateMap<TransportDto, Transport>().ReverseMap();
            CreateMap<FlightDto, Flight>().ReverseMap();
        }
    }
}