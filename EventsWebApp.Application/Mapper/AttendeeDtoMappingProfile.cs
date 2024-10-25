using AutoMapper;
using EventsWebApp.Domain.Models;
using EventsWebApp.Application.Dto;

namespace EventsWebApp.Application.Mapper
{
    public class AttendeeDtoMappingProfile : Profile
    {
        public AttendeeDtoMappingProfile() {
            CreateMap<Attendee, AttendeeDto>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("o")))
                .ForMember(dest => dest.DateOfRegistration, opt => opt.MapFrom(src => src.DateOfRegistration.ToString("o")))
                .ForMember(dest => dest.SocialEventName, opt => opt.MapFrom(src => src.SocialEvent.EventName))
                ;
        }
    }
}
