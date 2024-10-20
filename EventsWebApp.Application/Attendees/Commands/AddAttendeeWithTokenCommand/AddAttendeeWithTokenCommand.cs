using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Attendees.Commands
{
    public record AddAttendeeWithTokenCommand(AddUpdateAttendeeRequest Request, Guid EventId, string AccessToken) : ICommand<Guid>;
}
