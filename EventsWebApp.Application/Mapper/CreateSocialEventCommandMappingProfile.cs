using AutoMapper;
using EventsWebApp.Application.UseCases.SocialEvents.Commands;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class CreateSocialEventCommandMappingProfile : Profile
    {
        public CreateSocialEventCommandMappingProfile()
        {
            CreateMap<CreateSocialEventCommand, SocialEvent>()
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
               .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .AfterMap((com, user) => { user.Image = (user.Image == null) ? string.Empty : user.Image; })
               ;
        }
    }
}
