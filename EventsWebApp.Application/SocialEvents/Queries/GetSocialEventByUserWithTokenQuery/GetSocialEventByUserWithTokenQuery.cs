using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.SocialEvents.Queries.GetSocialEventByUserWithTokenQuery
{
    public record GetSocialEventByUserWithTokenQuery(Guid Id, string Token) :IQuery<SocialEventDto>;
}
