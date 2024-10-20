using EventsWebApp.Domain.Enums;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class AddUpdateSocialEventValidator<T> : AbstractValidator<T> where T : AddUpdateSocialEventRequest
    {
        public AddUpdateSocialEventValidator()
        {
            RuleFor(socialEvent => socialEvent.EventName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(socialEvent => socialEvent.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(socialEvent => socialEvent.Date)
                .NotEmpty()
                .Must(date => DateTime.Parse(date) >= DateTime.Now);

            RuleFor(socialEvent => socialEvent.Place)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(socialEvent => socialEvent.Category)
                .NotEmpty()
                .Must(category => !category.Equals(E_SocialEventCategory.None));

            RuleFor(socialEvent => socialEvent.MaxAttendee)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(100_000);
        }
    }
}
