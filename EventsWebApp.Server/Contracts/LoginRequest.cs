namespace EventsWebApp.Server.Contracts
{
    public record LoginRequest(string email, 
                                string password);
}