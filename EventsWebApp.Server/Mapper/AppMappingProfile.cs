using AutoMapper;
using EventsWebApp.Server.Dto;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Contracts;
using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Server.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<User, UserDto>();

            CreateMap<CreateSocialEventRequest, SocialEvent>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.MaxAttendee, opt => opt.MapFrom(src => src.MaxAttendee))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                ; ;

            CreateMap<SocialEvent, SocialEventResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.MaxAttendee, opt => opt.MapFrom(src => src.MaxAttendee))
                //MAP TO CORRECT ATTENDEE IN LIST
                .ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.ListOfAttendees))
                ;

        }
    }
}
