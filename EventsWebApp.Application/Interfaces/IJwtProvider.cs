using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}