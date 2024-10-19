using AutoMapper;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.SocialEvents.Commands.AddAttendeeCommand
{
    public class AddAttendeeHandler : ICommandHandler<AddAttendeeCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public AddAttendeeHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddAttendeeCommand request, CancellationToken cancellationToken)
        {
            var attendee = _mapper.Map<Attendee>(request);
            attendee.DateOfRegistration = DateTime.Now.Date;

            cancellationToken.ThrowIfCancellationRequested();
            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetByIdTracking(socialEventId, cancellationToken);

            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }

            var attendeesList = socialEvent.ListOfAttendees;

            if (attendeesList == null)
            {
                attendeesList = new List<Attendee>();
            }
            cancellationToken.ThrowIfCancellationRequested();
            Attendee candidate = await _appUnitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEventId, attendee.Email, cancellationToken);
            if (candidate != null)
            {
                throw new SocialEventException("This attendee already in the list");
            }
            if (attendeesList.Count + 1 > socialEvent.MaxAttendee)
            {
                throw new SocialEventException("Max attendee number reached");
            }
            cancellationToken.ThrowIfCancellationRequested();
            var resultId = await _attendeeService.AddAttendee(attendee, socialEvent, userId, cancellationToken);

            _appUnitOfWork.Save();
            return resultId;
        }
    }
}
