using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Attendees.Queries
{
    public record GetAttendeeByIdQuery : IdRequest, IQuery<AttendeeDto>;
}
