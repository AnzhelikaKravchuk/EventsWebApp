namespace EventsWebApp.Domain.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string message) : base(message)
        {
        }
    }
}
