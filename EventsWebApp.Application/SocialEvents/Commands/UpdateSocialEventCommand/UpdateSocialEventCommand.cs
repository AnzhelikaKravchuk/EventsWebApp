using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public record UpdateSocialEventCommand : AddUpdateSocialEventRequest, ICommand<Guid>
    {
        public Guid Id { get; set; }
        public UpdateSocialEventCommand() { }
    }
}
