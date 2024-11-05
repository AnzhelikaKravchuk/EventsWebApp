using System.Net;

namespace EventsWebApp.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) :base(message)
        {
        }
    }
}