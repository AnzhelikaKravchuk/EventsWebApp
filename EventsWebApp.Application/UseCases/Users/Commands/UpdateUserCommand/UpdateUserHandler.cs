using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.Users.Commands
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

            User candidate = await _appUnitOfWork.UserRepository.GetById(request.Id, cancellationToken);
            if (candidate == null)
            {
                throw new UserException("No user was found");
            }

            User user = _mapper.Map(request, candidate);
            Guid userId = await _appUnitOfWork.UserRepository.Update(user, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            _appUnitOfWork.Save();
            return userId;
        }
    }
}
