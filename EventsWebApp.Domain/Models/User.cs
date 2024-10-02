using Microsoft.AspNetCore.Identity;

namespace EventsWebApp.Domain.Models
{
    public class User //: IdentityUser
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;
        public string? RefreshToken { get;  set; } = string.Empty;
        public DateTime? ExpiresRefreshToken { get;  set; } 

        public User() { }
        public User(string email, string passwordHash, string username, string role)
        {
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.Username = username;
            this.Role = role;
        }
    }
}
