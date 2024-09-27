namespace EventsWebApp.Domain.Models
{
    public class Attendee
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Surname { get; } = string.Empty;
        public string Email { get;  } = string.Empty;
        public DateTime DateOfBirth { get;  }
        public SocialEvent SocialEvent { get; set; }
        public User User { get; set; }

        private Attendee()
        {

        }

        public Attendee(string name, string surname, string email, DateTime dateOfBirth, SocialEvent? socialEvent, User? user)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            SocialEvent = socialEvent;
            User = user;
        }

    }
}
