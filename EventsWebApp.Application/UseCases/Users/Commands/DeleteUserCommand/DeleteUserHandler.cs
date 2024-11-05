using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        public DeleteUserHandler(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _appUnitOfWork.UserRepository.GetById(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("No user found");
            }

            int rowsDeleted = await _appUnitOfWork.UserRepository.Delete(request.Id, cancellationToken);

            if (rowsDeleted == 0)
            {
                throw new NotFoundException("User wasn't deleted");
            }

            cancellationToken.ThrowIfCancellationRequested();
            _appUnitOfWork.Save();
            return request.Id;
        }
    }
}
