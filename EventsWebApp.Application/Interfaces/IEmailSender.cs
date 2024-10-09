namespace EventsWebApp.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
    }
}
