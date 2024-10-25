namespace EventsWebApp.Domain.Exceptions
{
    public class TokenException : BadRequestException
    {
        public TokenException(string message):base(message) { }
    }
}
