using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityDTO, City>();

            CreateMap<RouteDTO, Route>();

            CreateMap<TravelPlanDTO, TravelPlan>()
                .ForMember(dest => dest.Date, map => map.MapFrom(source => source.Date.ToUniversalTime()));
        }
    }
}