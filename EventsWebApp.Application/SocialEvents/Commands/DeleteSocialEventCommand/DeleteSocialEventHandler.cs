using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public class DeleteSocialEventHandler : ICommandHandler<DeleteSocialEventCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        public DeleteSocialEventHandler(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task<Guid> Handle(DeleteSocialEventCommand request, CancellationToken cancellationToken)
        {
            int rowsDeleted = await _appUnitOfWork.SocialEventRepository.Delete(request.Id, cancellationToken);
            if (rowsDeleted == 0)
            {
                throw new SocialEventException("Social event wasn't deleted");
            }

            cancellationToken.ThrowIfCancellationRequested();
            _appUnitOfWork.Save();
            return request.Id;
        }
    }
}
