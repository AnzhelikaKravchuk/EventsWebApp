using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Users.Commands
{
    public record RegisterUserCommand : LoginRegisterUserRequest, ICommand<(string, string)>
    {
        public string Username { get; set; }

        public RegisterUserCommand(string email,
                                    string password,
                                    string username) : base(email, password)
        {
            Username = username;
        }
    }
}