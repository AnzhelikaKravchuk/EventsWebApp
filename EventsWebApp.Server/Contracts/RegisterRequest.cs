using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record RegisterRequest([Required] string email, [Required] string password, [Required] string username);
}