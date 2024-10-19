using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.SocialEvents.Queries.GetAttendeesByEventIdQuery
{
    public record GetAttendeesByEventIdQuery(Guid Id) : IQuery<List<AttendeeResponse>>;
}
