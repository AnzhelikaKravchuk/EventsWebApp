
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Attendees.Queries
{
    public record GetAllAttendeesQuery : IQuery<List<AttendeeDto>>
    {
        private string? accessToken;

        public GetAllAttendeesQuery(string? accessToken)
        {
            this.accessToken = accessToken;
        }
    }
}
