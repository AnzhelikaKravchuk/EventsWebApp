using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Domain.Models
{
    public class SocialEvent
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public DateTime Date { get; }
        public string Place { get; } = string.Empty;
        public E_SocialEventCategory Category { get; }
        public int MaxAttendee { get; }
        public List<Attendee>? Attendees { get; } = new List<Attendee>();
        public byte[] Image { get; }

        public SocialEvent(Guid guid, string name, string description, DateTime date, string place, E_SocialEventCategory category, int maxAttendee, byte[] image)
        {
            Id = guid;
            Name = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxAttendee = maxAttendee;
            Image = image;
        }
    }
}
