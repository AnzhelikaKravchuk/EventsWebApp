
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.Attendees.Queries
{
    public record GetAllAttendeesQuery : IQuery<List<AttendeeDto>>
    {
    }
}
