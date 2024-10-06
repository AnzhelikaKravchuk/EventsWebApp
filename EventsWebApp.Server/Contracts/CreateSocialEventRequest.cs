using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record CreateSocialEventRequest(string EventName, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category, 
                                            int MaxAttendee,
                                            IFormFile? File);
}