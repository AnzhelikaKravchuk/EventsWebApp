using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public record GetSocialEventByIdQuery : IdRequest, IQuery<SocialEventDto>
    {
        public GetSocialEventByIdQuery() { }
        public GetSocialEventByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
