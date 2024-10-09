namespace EventsWebApp.Server.Contracts
{
    public record RegisterRequest(string email, 
                                    string password, 
                                    string username);
}