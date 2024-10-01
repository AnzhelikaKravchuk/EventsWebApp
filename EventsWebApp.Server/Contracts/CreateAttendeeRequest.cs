using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record CreateAttendeeRequest([Required] string Name,
                                            [Required] string Surname,
                                            [Required] string Email,
                                            [Required] string DateOfBirth);
}