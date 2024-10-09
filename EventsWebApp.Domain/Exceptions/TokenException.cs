namespace EventsWebApp.Domain.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException(string message):base(message) { }
    }
}
