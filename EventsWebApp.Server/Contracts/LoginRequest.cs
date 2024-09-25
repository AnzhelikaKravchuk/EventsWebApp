using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Server.Contracts
{
    public record LoginRequest([Required] string email, [Required] string password);
}