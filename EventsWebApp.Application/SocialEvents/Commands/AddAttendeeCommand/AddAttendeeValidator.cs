using FluentValidation;

namespace EventsWebApp.Application.SocialEvents.Commands.AddAttendeeCommand
{
    public class AddAttendeeValidator : AbstractValidator<AddAttendeeCommand>
    {
        public AddAttendeeValidator() {
            RuleFor(attendee => attendee.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(attendee => attendee.Name)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(attendee => attendee.Surname)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(attendee => attendee.DateOfBirth)
                .NotEmpty()
                .Must(date => DateTime.Parse(date) < DateTime.Now);
        }
    }
}
