using EventsWebApp.Domain.Enums;

namespace EventsWebApp.Domain.Models
{
    public class SocialEvent
    {
        public Guid Id { get;  set; }
        public string EventName { get;  set; } = string.Empty;
        public string Description { get;  set; } = string.Empty;
        public DateTime Date { get;  set; }
        public string Place { get;  set; } = string.Empty;
        public E_SocialEventCategory Category { get;  set; }
        public int MaxAttendee { get;  set; }
        public List<Attendee> ListOfAttendees { get; set; } = [];
        public string Image { get; set; } = string.Empty;

        public SocialEvent() { }
        public SocialEvent(string name, string description, DateTime date, string place, E_SocialEventCategory category, int maxAttendee, string image)
        {
            EventName = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxAttendee = maxAttendee;
            Image = image;
        }

        public SocialEvent(Guid id, string name, string description, DateTime date, string place, E_SocialEventCategory category, int maxAttendee, string image, List<Attendee> attendees)
        {
            Id = id;
            EventName = name;
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
