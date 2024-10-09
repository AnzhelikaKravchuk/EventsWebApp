namespace EventsWebApp.Server.Contracts
{
    public record AttendeeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string DateOfRegistration { get; set; } = string.Empty;
        public string SocialEventName { get; set; } = string.Empty;
                                            
    }
}

   