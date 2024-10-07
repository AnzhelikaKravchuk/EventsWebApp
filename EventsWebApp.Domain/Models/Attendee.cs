namespace EventsWebApp.Domain.Models
{
    public class Attendee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }

        public Guid SocialEventId { get; set; }
        public SocialEvent SocialEvent { get; set; }
        public Guid UserId { get; set; }
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
