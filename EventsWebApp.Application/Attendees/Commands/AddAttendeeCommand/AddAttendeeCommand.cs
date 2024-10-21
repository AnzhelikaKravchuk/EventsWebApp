using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Attendees.Commands
{
    public record AddAttendeeCommand : AddUpdateAttendeeRequest, ICommand<Guid>
    {
        public Guid EventId { get; set; }   
        public Guid UserId { get; set; }   
        public AddAttendeeCommand(string name, string surname, string email, string dateOfBirth,Guid EventId, Guid UserId) : base(name, surname, email, dateOfBirth)
        {
            this.EventId = EventId;
            this.UserId = UserId;
        }
    }
}
