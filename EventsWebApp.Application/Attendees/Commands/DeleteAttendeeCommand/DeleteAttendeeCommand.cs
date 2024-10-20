using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Attendees.Commands
{
    public record DeleteAttendeeCommand : IdRequest, ICommand<Guid>;
}
