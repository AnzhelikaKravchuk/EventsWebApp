using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.Attendees.Commands
{
    public class DeleteAttendeeHandler : ICommandHandler<DeleteAttendeeCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        public DeleteAttendeeHandler(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task<Guid> Handle(DeleteAttendeeCommand request, CancellationToken cancellationToken)
        {
            Attendee attendee = await _appUnitOfWork.AttendeeRepository.GetByIdWithInclude(request.Id, cancellationToken);

            if (attendee == null)
            {
                throw new AttendeeException("No attendee found");
            }

            cancellationToken.ThrowIfCancellationRequested();

            await _appUnitOfWork.AttendeeRepository.Delete(request.Id, cancellationToken);

            _appUnitOfWork.Save();
            return request.Id;
        }
    }
}
