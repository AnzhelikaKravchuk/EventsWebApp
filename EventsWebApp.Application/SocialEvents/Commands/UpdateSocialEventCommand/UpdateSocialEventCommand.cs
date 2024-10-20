using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public record UpdateSocialEventCommand(Guid Id) : AddUpdateSocialEventRequest, ICommand<Guid>;
}
