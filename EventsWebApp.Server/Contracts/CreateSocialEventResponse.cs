using EventsWebApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record SocialEventResponse([Required] string Name, 
                                            [Required] string Description, 
                                            [Required] string Place, 
                                            [Required] string Date, 
                                            [Required] string Category, 
                                            [Required] int MaxAttendee,
                                            [Required] List<Attendee> Attendees);
}