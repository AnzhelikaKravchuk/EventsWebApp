using EventsWebApp.Application.Validators;
using FluentValidation;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class AddAttendeeWithTokenValidator : AbstractValidator<AddAttendeeWithTokenCommand>
    {
        public AddAttendeeWithTokenValidator()
        {
            RuleFor(attendee => attendee.Request)
                .NotEmpty();

            RuleFor(attendee => attendee.EventId)
                .NotEmpty();

            RuleFor(attendee => attendee.AccessToken)
                .NotEmpty();
        }
    }
}
