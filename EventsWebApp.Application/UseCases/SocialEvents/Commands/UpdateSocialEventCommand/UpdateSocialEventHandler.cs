using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

namespace EventsWebApp.Application.UseCases.SocialEvents.Commands
{
    public class UpdateSocialEventHandler : ICommandHandler<UpdateSocialEventCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public UpdateSocialEventHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Guid> Handle(UpdateSocialEventCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent candidate = await _appUnitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, cancellationToken);
            if (candidate == null)
            {
                throw new NotFoundException("No social event found");
            }

            if (request.MaxAttendee < candidate.ListOfAttendees.Count)
            {
                throw new ConflictException("Can't lower max attendee number");
            }

            cancellationToken.ThrowIfCancellationRequested();
            bool isDateChanged = !candidate.Date.Equals(DateTime.Parse(request.Date));
            bool isPlaceChanged = !candidate.Place.Equals(request.Place);
            SocialEvent socialEvent = _mapper.Map(request, candidate);
            Guid id = await _appUnitOfWork.SocialEventRepository.Update(socialEvent, cancellationToken);

            _appUnitOfWork.Save();

            cancellationToken.ThrowIfCancellationRequested();
            if (isDateChanged || isPlaceChanged)
            {
                candidate.ListOfAttendees.ForEach((attendee) => _emailSender.SendEmailAsync(attendee.Email, "One of the social events that you applied to were changed!", $"Event {socialEvent.EventName} now has current Date of Event is {socialEvent.Date}, current Place of Event is {socialEvent.Place}"));
            }
            return id;
        }
    }
}
