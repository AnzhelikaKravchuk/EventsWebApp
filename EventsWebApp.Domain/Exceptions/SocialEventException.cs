namespace EventsWebApp.Domain.Exceptions
{
    public class SocialEventException : BadRequestException
    {
        public SocialEventException(string message):base(message) { }
    }
}
