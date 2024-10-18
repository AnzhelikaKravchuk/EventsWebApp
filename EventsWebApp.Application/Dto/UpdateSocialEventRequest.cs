using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.Dto
{
    public record UpdateSocialEventRequest(string Id, 
                                            string EventName, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category,
                                            string? Image,
                                            int MaxAttendee,
                                            IFormFile? File);
}