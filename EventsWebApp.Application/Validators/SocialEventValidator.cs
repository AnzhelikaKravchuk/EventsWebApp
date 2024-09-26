using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class SocialEventValidator : AbstractValidator<SocialEvent>
    {
        public SocialEventValidator() { 
            RuleFor(socialEvent => socialEvent.Id).NotEmpty();

            RuleFor(socialEvent => socialEvent.Name).NotEmpty().MaximumLength(100);
            RuleFor(socialEvent => socialEvent.Description).NotEmpty();
            RuleFor(socialEvent => socialEvent.Date).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(socialEvent => socialEvent.Place).NotEmpty().MaximumLength(100);
            RuleFor(socialEvent => socialEvent.Category).NotEmpty().Must(category => !category.Equals(E_SocialEventCategory.None));
            RuleFor(socialEvent => socialEvent.MaxAttendee).NotEmpty().LessThanOrEqualTo(100_000);
            RuleFor(socialEvent => socialEvent.Image).NotEmpty();
        }
    }
}
