using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands
{
    public record LoginUserCommand(string Email, string Password) : ICommand<(string, string)>;
}
