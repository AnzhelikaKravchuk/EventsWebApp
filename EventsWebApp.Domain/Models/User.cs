using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Domain.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Email { get;  set; } = string.Empty;
        public string PasswordHash { get;  set; } = string.Empty;
        public string Username { get;  set; } = string.Empty;
        public E_Role Role { get;  set; }
        public string? RefreshToken { get;  set; } = string.Empty;
        public DateTime? ExpiresRefreshToken { get;  set; } 

        public User() { }
        public User(string email, string passwordHash, string username, E_Role role)
        {
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.Username = username;
            this.Role = role;
        }
    }
}
