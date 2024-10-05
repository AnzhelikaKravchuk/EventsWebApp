namespace EventsWebApp.Server.Contracts
{
    public record UpdateSocialEventRequest(string Id, 
                                            string NameOfEvent, 
                                            string Description, 
                                            string Place, 
                                            string Date, 
                                            string Category, 
                                            int MaxAttendee);
}