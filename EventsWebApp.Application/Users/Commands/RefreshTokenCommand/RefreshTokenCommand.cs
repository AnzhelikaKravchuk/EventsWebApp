using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands.RefreshTokenCommand
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<string>;
}
