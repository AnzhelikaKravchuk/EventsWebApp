namespace EventsWebApp.Domain.Exceptions
{
    public class AttendeeException : BadRequestException
    {
        public AttendeeException(string message):base(message) { }
    }
}
