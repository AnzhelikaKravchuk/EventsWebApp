using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Attendees.Commands
{
    public record AddAttendeeWithTokenCommand : AddUpdateAttendeeRequest, ICommand<Guid>
    {
        public Guid EventId { get; set; }
        public string AccessToken { get; set; }
        public AddAttendeeWithTokenCommand(string name, string surname, string email, string dateOfBirth, Guid EventId, string accessToken) : base(name, surname, email, dateOfBirth)
        {
            this.EventId = EventId;
            this.AccessToken = accessToken;
        }
    }
}
