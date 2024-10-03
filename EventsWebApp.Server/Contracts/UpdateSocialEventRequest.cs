namespace EventsWebApp.Server.Contracts
{
    public record UpdateSocialEventRequest(Guid Id, 
                                            string Name, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category, 
                                            int MaxAttendee);
}