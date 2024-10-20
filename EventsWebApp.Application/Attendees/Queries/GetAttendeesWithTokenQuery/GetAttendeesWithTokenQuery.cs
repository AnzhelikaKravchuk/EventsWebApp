using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Attendees.Queries
{
    public record GetAttendeesWithTokenQuery(string AccessToken) : IQuery<List<AttendeeDto>>;
}
