using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Attendees.Commands
{
    public record AddAttendeeCommand(AddUpdateAttendeeRequest Request, Guid EventId, Guid UserId) : ICommand<Guid>;
}
