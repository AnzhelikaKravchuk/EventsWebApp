using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands.DeleteUserCommand
{
    public record DeleteUserCommand(Guid Id) : ICommand<Guid>;
}
