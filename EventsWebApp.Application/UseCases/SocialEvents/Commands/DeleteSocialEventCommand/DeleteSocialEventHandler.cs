using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.SocialEvents.Commands
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
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event found");
            }

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
