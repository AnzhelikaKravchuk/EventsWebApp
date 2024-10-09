namespace EventsWebApp.Server.Contracts
{
    public record CreateAttendeeRequest(string Name,
                                            string Surname,
                                            string Email,
                                            string DateOfBirth);
}