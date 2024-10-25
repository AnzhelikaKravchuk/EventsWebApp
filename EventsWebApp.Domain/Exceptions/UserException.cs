namespace EventsWebApp.Domain.Exceptions
{
    public class UserException : BadRequestException
    {
        public UserException(string message) :base(message){ }
    }
}
