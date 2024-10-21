using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class AddUpdateAttendeeValidator<T> : AbstractValidator<T>  where T : AddUpdateAttendeeRequest
    {
        public AddUpdateAttendeeValidator() : base()
        {
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
