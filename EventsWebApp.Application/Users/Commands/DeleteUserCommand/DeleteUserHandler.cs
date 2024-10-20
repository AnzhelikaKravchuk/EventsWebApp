using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.Users.Commands
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
            int rowsDeleted = await _appUnitOfWork.UserRepository.Delete(request.Id, cancellationToken);

            if (rowsDeleted == 0)
            {
                throw new SocialEventException("User wasn't deleted");
            }

            cancellationToken.ThrowIfCancellationRequested();
            _appUnitOfWork.Save();
            return request.Id;
        }
    }
}
