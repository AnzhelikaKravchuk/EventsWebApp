using EventsWebApp.Application.Validators;
using FluentValidation;

namespace EventsWebApp.Application.UseCases.Attendees.Commands
{
    public class AddAttendeeWithTokenValidator : AddUpdateAttendeeValidator<AddAttendeeWithTokenCommand>
    {
        public AddAttendeeWithTokenValidator() : base()
        {
            RuleFor(attendee => attendee.EventId)
                .NotEmpty();

            RuleFor(attendee => attendee.AccessToken)
                .NotEmpty();
        }
    }
}
