using EventsWebApp.Domain.Models;
using System.Security.Claims;

namespace EventsWebApp.Application.Interfaces
{
    public interface IJwtProvider
    {
        (string, string) CreateTokens(User user);
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}