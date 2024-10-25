using AutoMapper;
using EventsWebApp.Application.Helpers;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.UseCases.Attendees.Commands;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.Attendees.CommandsUserRepository.Get
{
    public class AddAttendeeWithTokenHandler : ICommandHandler<AddAttendeeWithTokenCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public AddAttendeeWithTokenHandler(IJwtProvider jwtProvider, IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _appUnitOfWork = appUnitOfWork;
        }

        public async Task<Guid> Handle(AddAttendeeWithTokenCommand request, CancellationToken cancellationToken)
        {
            Guid userId = TokenHelper.CheckToken(request.AccessToken, _jwtProvider);
            Attendee attendee = _mapper.Map<Attendee>(new AddUpdateAttendeeRequest(request.Name, request.Surname, request.Email, request.DateOfBirth));
            attendee.DateOfRegistration = DateTime.Now.Date;
            cancellationToken.ThrowIfCancellationRequested();

            (Attendee? candidate, SocialEvent? socialEvent) = await _appUnitOfWork.SocialEventRepository.GetAttendeeWithEventByEmail(request.EventId, attendee.Email, cancellationToken);

            if (socialEvent == null)
            {
                throw new SocialEventException("No social event found");
            }

            ValidateSocialEventAndCandidate(socialEvent, candidate, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _appUnitOfWork.UserRepository.GetByIdTracking(userId, cancellationToken);

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
