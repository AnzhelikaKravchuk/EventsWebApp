using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.SocialEvents.Commands
{
    public class CreateSocialEventValidator : AddUpdateSocialEventValidator<CreateSocialEventCommand>
    {
        public CreateSocialEventValidator() : base()
        {

        }
    }
}
