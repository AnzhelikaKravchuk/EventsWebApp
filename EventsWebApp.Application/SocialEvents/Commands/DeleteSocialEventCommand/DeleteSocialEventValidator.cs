using FluentValidation;

namespace EventsWebApp.Application.SocialEvents.Commands.DeleteSocialEventCommand
{
    public class DeleteSocialEventValidator : AbstractValidator<DeleteSocialEventCommand>
    {
        public DeleteSocialEventValidator() {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
