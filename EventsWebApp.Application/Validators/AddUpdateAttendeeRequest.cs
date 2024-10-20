namespace EventsWebApp.Application.Validators
{
    public record AddUpdateAttendeeRequest(string Name, string Surname, string Email, string DateOfBirth);
}
