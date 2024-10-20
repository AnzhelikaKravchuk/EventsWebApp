using FluentValidation;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class AddAttendeeValidator : AbstractValidator<AddAttendeeCommand>
    {
        public AddAttendeeValidator() : base()
        {
            RuleFor(attendee => attendee.Request)
                .NotEmpty();

            RuleFor(attendee => attendee.EventId)
                .NotEmpty();

            RuleFor(attendee => attendee.UserId)
                .NotEmpty();


            RuleFor(attendee => attendee.Request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(attendee => attendee.Request.Name)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(attendee => attendee.Request.Surname)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(attendee => attendee.Request.DateOfBirth)
                .NotEmpty()
                .Must(date => DateTime.Parse(date) < DateTime.Now);
        }
    }
}
