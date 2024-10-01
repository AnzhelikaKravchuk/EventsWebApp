using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record UpdateSocialEventRequest([Required] Guid Id, 
                                            [Required] string Name, 
                                            [Required] string Description, 
                                            [Required] string Place, 
                                            [Required] string Date, 
                                            [Required] string Category, 
                                            [Required] int MaxAttendee);
}