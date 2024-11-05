using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

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
            Attendee attendee = await _appUnitOfWork.AttendeeRepository.GetById(request.Id, cancellationToken);

            if (attendee == null)
            {
                throw new NotFoundException("No attendee found");
            }

            cancellationToken.ThrowIfCancellationRequested();

            int rowsDeleted = await _appUnitOfWork.AttendeeRepository.Delete(request.Id, cancellationToken);

            if (rowsDeleted == 0)
            {
                throw new NotFoundException("Attendee wasn't deleted");
            }

            _appUnitOfWork.Save();
            return request.Id;
        }
    }
}
