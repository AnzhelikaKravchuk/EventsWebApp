
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Infrastructure.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public User ToDomainUser()
        {
            return new User(Id, Email, PasswordHash, Username, Role);
        }
    }
}
