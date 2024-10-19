using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.SocialEvents.Commands.DeleteSocialEventCommand
{
    public record class DeleteSocialEventCommand(Guid Id) :ICommand<Guid>;
}
