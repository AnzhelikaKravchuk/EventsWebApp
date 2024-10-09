using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using EventsWebApp.Application.Interfaces;

namespace EventsWebApp.Infrastructure.Handlers
{
    public class EmailSender : IEmailSender
    {
        private readonly string? email = string.Empty;
        private readonly string? password = string.Empty;
        public EmailSender(IConfiguration configuration)
        {
            email = configuration["SMTPServerEmail"];
            password = configuration["SMTPServerPassword"];
        }
        public Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("No email or password provided. Sending email is ignored.");
                return Task.CompletedTask;
            }

            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(new MailAddress(recipientEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = message;


            return client.SendMailAsync(mailMessage);
        }
    }
}
