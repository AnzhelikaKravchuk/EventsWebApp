using FluentValidation;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class UpdateAttendeeValidator : AbstractValidator<UpdateAttendeeCommand>
    {
        public UpdateAttendeeValidator() : base(){
            RuleFor(attendee => attendee.Request)
                .NotEmpty();

            RuleFor(attendee => attendee.Id)
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
