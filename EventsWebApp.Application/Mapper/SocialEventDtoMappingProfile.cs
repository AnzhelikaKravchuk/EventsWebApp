using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class SocialEventDtoMappingProfile : Profile
    {
        public SocialEventDtoMappingProfile()
        {
            CreateMap<SocialEvent, SocialEventDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("o")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                ;

        }
    }
}
