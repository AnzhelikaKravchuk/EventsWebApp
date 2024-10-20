using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Users.Commands
{
    public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = _passwordHasher.Generate(request.Password);

            var user = _mapper.Map<User>(request);
            var userId = await _appUnitOfWork.UserRepository.Update(user, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            _appUnitOfWork.Save();
            return userId;
        }
    }
}
