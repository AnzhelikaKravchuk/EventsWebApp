using System.Net;

namespace EventsWebApp.Domain.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message):base(message) { }
    }
}
