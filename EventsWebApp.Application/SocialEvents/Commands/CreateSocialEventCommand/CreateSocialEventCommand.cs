using EventsWebApp.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.SocialEvents.Commands.CreateSocialEventCommand
{
    public record CreateSocialEventCommand : ICommand<Guid>
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Place {  get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string? Image{ get; set; }
        public int MaxAttendee {  get; set; }
        public IFormFile? File { get; set; }

        public CreateSocialEventCommand() { }
        public CreateSocialEventCommand(string name,
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
