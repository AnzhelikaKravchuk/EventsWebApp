using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Application.Dto
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public E_Role Role { get; set; }

        public UserDto() { }
        public UserDto(string email, string username, E_Role role)
        {
            this.Email = email;
            this.Username = username;
            this.Role = role;
        }
    }
}
