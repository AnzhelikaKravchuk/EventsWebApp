namespace EventsWebApp.Domain.Models
{
    public class User
    {
        public Guid Id { get;  set; }
        public string Email { get;  set; } = string.Empty;
        public string PasswordHash { get;  set; } = string.Empty;
        public string Username { get;  set; } = string.Empty;
        public string Role { get;  set; } = string.Empty;
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
