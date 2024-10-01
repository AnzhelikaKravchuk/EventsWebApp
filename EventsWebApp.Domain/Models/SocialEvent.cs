using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Domain.Models
{
    public class SocialEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }
        public string Place { get; private set; } = string.Empty;
        public E_SocialEventCategory Category { get; private set; }
        public int MaxAttendee { get; private set; }
        public List<Attendee> ListOfAttendees { get; set; } = [];
        public byte[] Image { get; set; } = [];

        public SocialEvent() { }
        public SocialEvent(string name, string description, DateTime date, string place, E_SocialEventCategory category, int maxAttendee, byte[] image)
        {
            Name = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxAttendee = maxAttendee;
            Image = image;
        }

        public SocialEvent(Guid id, string name, string description, DateTime date, string place, E_SocialEventCategory category, int maxAttendee, byte[] image, List<Attendee> attendees)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxAttendee = maxAttendee;
            Image = image;
            ListOfAttendees = attendees;
        }
    }
}
