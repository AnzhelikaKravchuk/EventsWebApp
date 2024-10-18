namespace EventsWebApp.Application.Dto
{
    public record CreateAttendeeRequest(string Name,
                                            string Surname,
                                            string Email,
                                            string DateOfBirth);
}