using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<string>;
}
