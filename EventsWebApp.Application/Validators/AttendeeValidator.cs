using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class AttendeeValidator : AbstractValidator<Attendee>
    {
        public AttendeeValidator() {
            RuleFor(attendee => attendee.Id).NotEmpty();
            RuleFor(attendee => attendee.Email).NotEmpty().EmailAddress();
            RuleFor(attendee => attendee.Name).NotEmpty().MaximumLength(100);
            RuleFor(attendee => attendee.Surname).NotEmpty().MaximumLength(100);
            RuleFor(attendee => attendee.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            RuleFor(attendee => attendee.User).NotEmpty();
            RuleFor(attendee => attendee.SocialEvent).NotEmpty();
        }
    }
}
