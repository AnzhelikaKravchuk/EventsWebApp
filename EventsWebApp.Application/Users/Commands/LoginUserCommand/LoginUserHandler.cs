using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Users.Commands.LoginUserCommand
{
    public class LoginUserHandler : ICommandHandler<LoginUserCommand, (string,string)>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public LoginUserHandler(IAppUnitOfWork appUnitOfWork, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _appUnitOfWork = appUnitOfWork;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<(string, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User candidate = await _appUnitOfWork.UserRepository.GetByEmailTracking(request.Email, cancellationToken);

            if (candidate == null || !_passwordHasher.Verify(request.Password, candidate.PasswordHash))
            {
                throw new UserException("No candidate found");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var (accessToken, refreshToken) = _jwtProvider.CreateTokens(candidate);

            _appUnitOfWork.Save();
            return (accessToken, refreshToken);
        }
    }
}
