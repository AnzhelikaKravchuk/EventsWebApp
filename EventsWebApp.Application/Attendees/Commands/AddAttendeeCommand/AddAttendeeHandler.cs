using AutoMapper;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Attendees.Commands
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
            Attendee attendee = _mapper.Map<Attendee>(new AddUpdateAttendeeRequest(request.Name, request.Surname, request.Email, request.DateOfBirth));
            attendee.DateOfRegistration = DateTime.Now.Date;

            cancellationToken.ThrowIfCancellationRequested();

            (Attendee? candidate, SocialEvent socialEvent) = await _appUnitOfWork.SocialEventRepository.GetAttendeeWithEventByEmail(request.EventId, attendee.Email, cancellationToken);

            ValidateSocialEventAndCandidate(socialEvent, candidate, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _appUnitOfWork.UserRepository.GetByIdTracking(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new UserException("No user was found");
            }
            attendee.SocialEvent = socialEvent;
            attendee.User = user;

            cancellationToken.ThrowIfCancellationRequested();
            socialEvent.ListOfAttendees.Add(attendee);
            Guid resultId = await _appUnitOfWork.AttendeeRepository.Add(attendee, cancellationToken);
            _appUnitOfWork.Save();
            return resultId;
        }

        public void ValidateSocialEventAndCandidate(SocialEvent socialEvent, Attendee? candidate, CancellationToken cancellationToken)
        {
            List<Attendee> attendeesList = socialEvent.ListOfAttendees;
            if (attendeesList == null)
            {
                attendeesList = new List<Attendee>();
            }

            cancellationToken.ThrowIfCancellationRequested();
            if (candidate != null)
            {
                throw new SocialEventException("This attendee already in the list");
            }
            if (attendeesList.Count + 1 > socialEvent.MaxAttendee)
            {
                throw new SocialEventException("Max attendee number reached");
            }
        }
    }
}
