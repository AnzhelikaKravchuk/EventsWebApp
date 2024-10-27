using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public record UpdateUserCommand : IdRequest, ICommand<Guid>
    {
        public string Password { get; set; }
        public string Username { get; set; }

        public UpdateUserCommand() { }

        public UpdateUserCommand(Guid id,
                                    string password,
                                    string username)
        {
            Id = id;
            Password = password;
            Username = username;
        }
    }
}
