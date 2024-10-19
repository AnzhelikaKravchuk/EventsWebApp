using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.SocialEvents.Commands.AddAttendeeCommand
{
    public record AddAttendeeCommand(string Name, string Surname, string Email, string DateOfBirth) : ICommand<Guid>;
}
