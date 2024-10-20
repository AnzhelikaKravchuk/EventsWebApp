using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
namespace EventsWebApp.Application.Users.Commands
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, (string, string)>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public RegisterUserHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, UserValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<(string, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _appUnitOfWork.UserRepository.GetByEmailTracking(request.Email, cancellationToken);
            if (candidate != null)
            {
                throw new UserException("User already exists");
            }
            request.Password = _passwordHasher.Generate(request.Password);

            var user = _mapper.Map<User>(request);

            var addedUserId = await _appUnitOfWork.UserRepository.Add(user, cancellationToken);
            user.Id = addedUserId;

            cancellationToken.ThrowIfCancellationRequested();
            var (accessToken, refreshToken) = _jwtProvider.CreateTokens(user);
            _appUnitOfWork.Save();

            return (accessToken, refreshToken);
        }
    }
}
