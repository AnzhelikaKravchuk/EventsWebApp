using EventsWebApp.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.SocialEvents.Commands.UpdateSocialEventCommand
{
    public record UpdateSocialEventCommand : ICommand<Guid>
    {
            public Guid Id{ get; set; }
            public string EventName { get; set; }
            public string Description { get; set; }
            public string Place { get; set; }
            public string Date { get; set; }
            public string Category { get; set; }
            public string? Image { get; set; }
            public int MaxAttendee { get; set; }
            public IFormFile? File { get; set; }

            public UpdateSocialEventCommand() { }
            public UpdateSocialEventCommand(Guid id,
                                                string name,
                                                string description,
                                                string place,
                                                string date,
                                                string category,
                                                int maxAttendee,
                                                string? image,
                                                IFormFile? file)
            {
                Id = id;
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
