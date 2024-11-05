using EventsWebApp.Application.Helpers;
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
            Guid userId = TokenHelper.CheckToken(request.AccessToken, _jwtProvider);

            cancellationToken.ThrowIfCancellationRequested();
            var user = await _appUnitOfWork.UserRepository.GetById(userId, cancellationToken);

            if (user == null || user.RefreshToken != request.RefreshToken || user.ExpiresRefreshToken <= DateTime.UtcNow)
            {
                throw new UnauthorizedException("Refresh token is not valid or expired");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var accessToken = _jwtProvider.GenerateAccessToken(user);
            _appUnitOfWork.Save();
            return accessToken;
        }
    }
}
