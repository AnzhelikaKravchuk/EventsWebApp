using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Attendees.Commands
{
    public record UpdateAttendeeCommand : AddUpdateAttendeeRequest, ICommand<Guid>
    {
        public Guid Id { get; set; }
        public UpdateAttendeeCommand(string name, string surname, string email, string dateOfBirth, Guid id) : base(name, surname, email, dateOfBirth)
        {
            Id = id;
        }
    }
}
