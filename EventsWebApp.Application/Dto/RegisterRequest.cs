namespace EventsWebApp.Application.Dto
{
    public record RegisterRequest(string email, 
                                    string password, 
                                    string username);
}