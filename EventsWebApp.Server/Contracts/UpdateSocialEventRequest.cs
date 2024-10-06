namespace EventsWebApp.Server.Contracts
{
    public record UpdateSocialEventRequest(string Id, 
                                            string EventName, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category, 
                                            int MaxAttendee);
}