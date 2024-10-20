using AutoMapper;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Attendees.Commands
{
    public class UpdateAttendeeHandler : ICommandHandler<UpdateAttendeeCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public UpdateAttendeeHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateAttendeeCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Attendee attendee = _mapper.Map<Attendee>(request);
            var id = await _appUnitOfWork.AttendeeRepository.Update(attendee, cancellationToken);

            _appUnitOfWork.Save();
            return id;
        }
    }
}
