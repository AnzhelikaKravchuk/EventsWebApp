using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public record DeleteSocialEventCommand : IdRequest, ICommand<Guid>
    {
        public DeleteSocialEventCommand() { }
        public DeleteSocialEventCommand(Guid id)
        {
            Id = id;
        }
    }
}
