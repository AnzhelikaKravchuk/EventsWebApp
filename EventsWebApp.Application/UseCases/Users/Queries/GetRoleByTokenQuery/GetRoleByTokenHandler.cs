using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using System.Security.Claims;

namespace EventsWebApp.Application.UseCases.Users.Queries
{
    public class GetRoleByTokenHandler : IQueryHandler<GetRoleByTokenQuery, string?>
    {
        private readonly IJwtProvider _jwtProvider;
        public GetRoleByTokenHandler(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public Task<string?> Handle(GetRoleByTokenQuery request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);
        }
    }
}
