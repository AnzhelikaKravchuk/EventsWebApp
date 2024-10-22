using AutoMapper;
using EventsWebApp.Domain.Models;
using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Application.Validators;
using EventsWebApp.Application.UseCases.Users.Commands;
using EventsWebApp.Application.UseCases.SocialEvents.Commands;

namespace EventsWebApp.Application.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<User, UserDto>();

            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .AfterMap((com, user)=>user.Role = E_Role.User)
                ;

            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateSocialEventCommand, SocialEvent>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .AfterMap((com, user) => { user.Image = (user.Image == null) ? string.Empty : user.Image; })
                ;

            CreateMap<SocialEvent, SocialEventDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("o")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                ;

            CreateMap<UpdateSocialEventCommand, SocialEvent>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date).Date))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                ;

            CreateMap<AddUpdateAttendeeRequest, Attendee>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfRegistration, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.SocialEvent, opt => opt.Ignore())
                ;

            CreateMap<Attendee, AttendeeDto>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("o")))
                .ForMember(dest => dest.DateOfRegistration, opt => opt.MapFrom(src => src.DateOfRegistration.ToString("o")))
                .ForMember(dest => dest.SocialEventName, opt => opt.MapFrom(src => src.SocialEvent.EventName))
                ;
        }
    }
}
