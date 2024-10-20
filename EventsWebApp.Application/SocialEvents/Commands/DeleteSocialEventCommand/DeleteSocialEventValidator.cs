using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public class DeleteSocialEventValidator : IdValidator<DeleteSocialEventCommand>
    {
        public DeleteSocialEventValidator() : base() {
        }
    }
}
