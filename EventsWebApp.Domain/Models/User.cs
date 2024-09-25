namespace EventsWebApp.Domain.Models
{
    public class User
    {
        public Guid Id { get; }
        public string Email { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;
        public string Username { get; } = string.Empty;
        public string Role { get; } = string.Empty;

        public User(Guid id, string email, string passwordHash, string username, string role)
        {
            this.Id = id;
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.Username = username;
            this.Role = role;
        }
    }
}
