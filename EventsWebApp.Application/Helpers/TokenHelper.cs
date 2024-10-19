using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Exceptions;

namespace EventsWebApp.Application.Helpers
{
    public static class TokenHelper
    {
        public static Guid CheckToken(string accessToken, IJwtProvider jwtProvider)
        {
            var principal = jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                throw new TokenException("Invalid token");
            }
            return Guid.Parse(userId);
        }
    }
}
