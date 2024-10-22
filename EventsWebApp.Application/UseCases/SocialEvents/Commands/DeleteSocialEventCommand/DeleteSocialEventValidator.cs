using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.SocialEvents.Commands
{
    public class DeleteSocialEventValidator : IdValidator<DeleteSocialEventCommand>
    {
        public DeleteSocialEventValidator() : base()
        {
        }
    }
}
