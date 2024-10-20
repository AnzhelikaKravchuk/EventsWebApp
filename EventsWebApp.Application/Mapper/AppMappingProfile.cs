using AutoMapper;
using EventsWebApp.Domain.Models;
using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Application.SocialEvents.Commands;
using EventsWebApp.Application.Users.Commands;
using EventsWebApp.Application.Attendees.Commands;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<User, UserDto>();

            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .AfterMap((com, user)=>user.Role = E_Role.User)
                ;

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                ;

            CreateMap<CreateSocialEventCommand, SocialEvent>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.MaxAttendee, opt => opt.MapFrom(src => src.MaxAttendee))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                ;

            CreateMap<SocialEvent, SocialEventDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("o")))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.MaxAttendee, opt => opt.MapFrom(src => src.MaxAttendee))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.MapFrom(src => src.ListOfAttendees))
                ;

            CreateMap<UpdateSocialEventCommand, SocialEvent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date).Date))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.MaxAttendee, opt => opt.MapFrom(src => src.MaxAttendee)) 
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                ;

            CreateMap<AddUpdateAttendeeRequest, Attendee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfRegistration, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.SocialEvent, opt => opt.Ignore())
                ;

            CreateMap<Attendee, AttendeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString()))
                .ForMember(dest => dest.DateOfRegistration, opt => opt.MapFrom(src => src.DateOfRegistration.ToString()))
                .ForMember(dest => dest.SocialEventName, opt => opt.MapFrom(src => src.SocialEvent.EventName))
                ;
        }
    }
}
