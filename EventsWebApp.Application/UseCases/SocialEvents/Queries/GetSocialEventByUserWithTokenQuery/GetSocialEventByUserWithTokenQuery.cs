using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public record GetSocialEventByUserWithTokenQuery(Guid Id, string Token) : IQuery<SocialEventDto>;
}
