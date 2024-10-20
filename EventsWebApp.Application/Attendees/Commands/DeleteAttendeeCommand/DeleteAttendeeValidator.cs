using EventsWebApp.Application.Validators;
using FluentValidation;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class DeleteAttendeeValidator : IdValidator<DeleteAttendeeCommand>
    {
        public DeleteAttendeeValidator() : base()
        {
        }
    }
}
