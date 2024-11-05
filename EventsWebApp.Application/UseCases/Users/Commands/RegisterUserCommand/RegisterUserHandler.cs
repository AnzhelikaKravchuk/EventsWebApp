using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;
namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, (string, string)>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public RegisterUserHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<(string, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User candidate = await _appUnitOfWork.UserRepository.GetByEmail(request.Email, cancellationToken);
            if (candidate != null)
            {
                throw new NotFoundException("User already exists");
            }
            request.Password = _passwordHasher.Generate(request.Password);

            User user = _mapper.Map<User>(request);

            Guid addedUserId = await _appUnitOfWork.UserRepository.Add(user, cancellationToken);
            user.Id = addedUserId;

            cancellationToken.ThrowIfCancellationRequested();
            (string accessToken, string refreshToken) = _jwtProvider.CreateTokens(user);
            _appUnitOfWork.Save();

            return (accessToken, refreshToken);
        }
    }
}
