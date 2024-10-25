using AutoMapper;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class AddUpdateAttendeeRequestMappingProfile : Profile
    {
        public AddUpdateAttendeeRequestMappingProfile()
        {
            CreateMap<AddUpdateAttendeeRequest, Attendee>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfRegistration, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.SocialEvent, opt => opt.Ignore())
                ;
        }
    }
}
