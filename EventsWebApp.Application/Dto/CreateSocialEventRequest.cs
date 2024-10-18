using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.Dto
{
    public record CreateSocialEventRequest(string EventName, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category, 
                                            int MaxAttendee,
                                            IFormFile? File);
}