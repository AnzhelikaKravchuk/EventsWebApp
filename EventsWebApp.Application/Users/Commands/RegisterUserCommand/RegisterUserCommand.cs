using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Commands
{
    public record RegisterUserCommand : ICommand<(string, string)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public RegisterUserCommand(string email,
                                    string password,
                                    string username)
        {
            Email = email;
            Password = password;
            Username = username;
        }
    }
}