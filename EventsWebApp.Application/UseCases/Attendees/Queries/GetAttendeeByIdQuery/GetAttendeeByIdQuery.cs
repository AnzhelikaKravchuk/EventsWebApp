using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Attendees.Queries
{
    public record GetAttendeeByIdQuery : IdRequest, IQuery<AttendeeDto>
    {
        public GetAttendeeByIdQuery() { }

        public GetAttendeeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
