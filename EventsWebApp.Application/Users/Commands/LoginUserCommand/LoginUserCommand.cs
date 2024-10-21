using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Users.Commands
{
    public record LoginUserCommand : LoginRegisterUserRequest, ICommand<(string, string)>
    {
        public LoginUserCommand() { }
    }
}
