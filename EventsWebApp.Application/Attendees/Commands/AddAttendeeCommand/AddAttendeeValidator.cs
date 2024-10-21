using EventsWebApp.Application.Validators;
using FluentValidation;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class AddAttendeeValidator : AddUpdateAttendeeValidator<AddAttendeeCommand>
    {
        public AddAttendeeValidator() : base()
        {
            RuleFor(attendee => attendee.EventId)
                .NotEmpty();

            RuleFor(attendee => attendee.UserId)
                .NotEmpty();

        }
    }
}
