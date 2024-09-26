namespace EventsWebApp.Domain.Models
{
    public class Attendee
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Surname { get; } = string.Empty;
        public string Email { get;  } = string.Empty;
        public DateTime DateOfBirth { get;  }
        public SocialEvent? SocialEvent { get; }
        public User? User { get; }
    }
}
