using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.Attendees.Queries
{
    public record GetAttendeesWithTokenQuery(string AccessToken) : IQuery<List<AttendeeDto>>;
}
