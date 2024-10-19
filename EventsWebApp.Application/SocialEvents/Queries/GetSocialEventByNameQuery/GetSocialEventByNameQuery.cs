using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.SocialEvents.Queries.GetSocialEventByIdQuery
{
    public record GetSocialEventByNameQuery(string Name) : IQuery<SocialEventDto>;
}
