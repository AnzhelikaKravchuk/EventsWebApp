namespace EventsWebApp.Domain.Models
{
    public class Attendee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public DateTime DateOfRegistration { get; set; }
        public SocialEvent SocialEvent { get; set; }
        public User User { get; set; }

        public Attendee()
        {

        }

        public Attendee(string name, string surname, string email, DateTime dateOfBirth,DateTime dateOfRegistration, SocialEvent? socialEvent, User? user)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            DateOfRegistration = dateOfRegistration;
            SocialEvent = socialEvent;
            User = user;
        }

    }
}
