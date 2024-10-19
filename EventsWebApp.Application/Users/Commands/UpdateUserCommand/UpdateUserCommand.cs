using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands.UpdateUserCommand
{
    public record UpdateUserCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

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
