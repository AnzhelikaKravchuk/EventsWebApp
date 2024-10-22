using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.Validators
{
    public record AddUpdateSocialEventRequest
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string? Image { get; set; }
        public int MaxAttendee { get; set; }
        public IFormFile? File { get; set; }

        public AddUpdateSocialEventRequest() { }
        public AddUpdateSocialEventRequest(string name,
                                            string description,
                                            string place,
                                            string date,
                                            string category,
                                            int maxAttendee,
                                            string? image,
                                            IFormFile? file)
        {
            EventName = name;
            Description = description;
            Place = place;
            Date = date;
            Category = category;
            MaxAttendee = maxAttendee;
            File = file;
            Image = image;
        }
    }
}