namespace EventsWebApp.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public UserDto(Guid id, string email, string username, string role) {
            Id = id;
            Email = email; 
            Username = username; 
            Role = role;
        }
    }
}
