using AutoMapper;
using EventsWebApp.Application.UseCases.SocialEvents.Commands;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
namespace EventsWebApp.Application.Mapper
{
    public class UpdateSocialEventCommandMappingProfile : Profile
    {
        public UpdateSocialEventCommandMappingProfile()
        {
            CreateMap<UpdateSocialEventCommand, SocialEvent>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date).Date))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), src.Category)))
                .ForMember(dest => dest.ListOfAttendees, opt => opt.Ignore())
                .AfterMap((com, res) => { res.Image = (res.Image == null) ? string.Empty : res.Image; })
                ;
        }
    }
}
