using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public record GetAttendeesByEventIdQuery : IdRequest, IQuery<List<AttendeeDto>>
    {
        public GetAttendeesByEventIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
