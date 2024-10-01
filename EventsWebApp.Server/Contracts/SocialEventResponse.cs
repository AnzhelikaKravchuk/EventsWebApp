using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Server.Contracts
{
    public record SocialEventResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Date { get; set; }
        public string Place { get; set; } = string.Empty;
        public string Category { get; set; }
        public int MaxAttendee { get; set; }
        public List<Attendee> ListOfAttendees { get; set; } = [];
        public byte[] Image { get; set; }

        public SocialEventResponse() { }

        public SocialEventResponse(Guid id, string name,
                                            string description,
                                            string place,
                                            string date,
                                            string category,
                                            int maxAttendee,
                                            List<Attendee> attendees)
        {
            Id = id;
            Name = name;
            Description = description;
            Place = place;
            Date = date;
            Category = category;
            MaxAttendee = maxAttendee;
            ListOfAttendees = attendees;

        }
    }
}