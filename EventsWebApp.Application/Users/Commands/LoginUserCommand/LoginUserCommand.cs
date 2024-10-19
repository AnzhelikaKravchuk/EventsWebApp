using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands.LoginUserCommand
{
    public record LoginUserCommand(string Email, string Password) : ICommand<(string, string)>;
}
