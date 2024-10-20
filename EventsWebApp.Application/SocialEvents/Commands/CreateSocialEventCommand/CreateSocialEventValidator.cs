using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public class CreateSocialEventValidator : AddUpdateSocialEventValidator<CreateSocialEventCommand>
    {
        public CreateSocialEventValidator() : base() { 
           
        }
    }
}
