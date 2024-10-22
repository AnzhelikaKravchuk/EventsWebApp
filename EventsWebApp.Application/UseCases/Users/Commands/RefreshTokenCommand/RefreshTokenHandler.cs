using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, string>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IJwtProvider _jwtProvider;
        public RefreshTokenHandler(IAppUnitOfWork appUnitOfWork, IJwtProvider jwtProvider)
        {
            _appUnitOfWork = appUnitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                throw new UserException("No user id found");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var user = await _appUnitOfWork.UserRepository.GetById(Guid.Parse(userId), cancellationToken);

            if (user == null || user.RefreshToken != request.RefreshToken || user.ExpiresRefreshToken <= DateTime.UtcNow)
            {
                throw new TokenException("Invalid token");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var accessToken = _jwtProvider.GenerateAccessToken(user);
            _appUnitOfWork.Save();
            return accessToken;
        }
    }
}
