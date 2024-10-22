using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.UseCases.SocialEvents.Commands
{
    public record CreateSocialEventCommand : AddUpdateSocialEventRequest, ICommand<Guid>
    {
        public CreateSocialEventCommand() { }
        public CreateSocialEventCommand(string name, string description, string place, string date, string category, int maxAttendee, string? image, IFormFile? file) : base(name, description, place, date, category, maxAttendee, image, file)
        {
        }
    }
}
