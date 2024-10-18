using EventsWebApp.Domain.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class SocialEventService : IDisposable, ISocialEventService
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IAttendeeService _attendeeService;
        private readonly IEmailSender _emailSender;
        private readonly SocialEventValidator _validator;
        public SocialEventService(IAppUnitOfWork appUnitOfWork, IJwtProvider jwtProvider, IAttendeeService attendeeService, IEmailSender emailSender, SocialEventValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _jwtProvider = jwtProvider;
            _attendeeService = attendeeService;
            _emailSender = emailSender;
            _validator = validator;
        }

        public async Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex = 1, int pageSize = 10)
        {
            var socialEvents = await _appUnitOfWork.SocialEventRepository.GetSocialEvents(filters, pageIndex, pageSize);
            return socialEvents;
        }

        public async Task<SocialEvent> GetSocialEventById(Guid id)
        {
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(id);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }
            return socialEvent;
        }

        public async Task<SocialEvent> GetSocialEventByName(string name)
        {
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByName(name);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }
            return socialEvent;
        }

        public async Task<(SocialEvent, bool)> GetSocialEventByIdWithToken(Guid id, string accessToken)
        {
            var userId = CheckToken(accessToken);
            var socialEvent = await GetSocialEventById(id);
            var attendee = socialEvent.ListOfAttendees.FirstOrDefault((a) => a.UserId == userId);
            return (socialEvent, attendee != null);
        }


        public async Task<List<Attendee>> GetAttendeesById(Guid id)
        {
            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(id);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }
            return socialEvent.ListOfAttendees;
        }

        public async Task<Guid> CreateSocialEvent(SocialEvent socialEvent)
        {
            ValidateSocialEvent(socialEvent);

            var id = await _appUnitOfWork.SocialEventRepository.Add(socialEvent);
            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> UpdateSocialEvent(SocialEvent socialEvent)
        {
            var candidate = await _appUnitOfWork.SocialEventRepository.GetById(socialEvent.Id);
            if (candidate == null)
            {
                throw new SocialEventException("No social event found");
            }

            if (socialEvent.MaxAttendee < candidate.ListOfAttendees.Count)
            {
                throw new SocialEventException("Can't lower max attendee number");
            }

            ValidateSocialEvent(socialEvent);

            bool isDateChanged = !candidate.Date.Equals(socialEvent.Date);
            bool isPlaceChanged = candidate.Place != socialEvent.Place;
            var id = await _appUnitOfWork.SocialEventRepository.Update(socialEvent);

            _appUnitOfWork.Save();

            if(isDateChanged || isPlaceChanged)
            {
                candidate.ListOfAttendees.ForEach((attendee) => _emailSender.SendEmailAsync(attendee.Email, "One of the social events that you applied to were changed!", $"Event {socialEvent.EventName} now has current Date of Event is {socialEvent.Date}, current Place of Event is {socialEvent.Place}"));
            }
            return id;
        }

        public async Task<Guid> AddAttendeeToEvent(Guid socialEventId, Attendee attendee, Guid userId)
        {
            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetByIdTracking(socialEventId);

            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }

            var attendeesList = socialEvent.ListOfAttendees;

            if (attendeesList == null)
            {
                attendeesList = new List<Attendee>();
            }
            Attendee candidate = await _appUnitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEventId, attendee.Email);
            if (candidate != null)
            {
                throw new SocialEventException("This attendee already in the list");
            }
            if (attendeesList.Count + 1 > socialEvent.MaxAttendee)
            {
                throw new SocialEventException("Max attendee number reached");
            }

            var resultId = await _attendeeService.AddAttendee(attendee, socialEvent, userId);

            _appUnitOfWork.Save();
            return resultId;
        }

        public async Task<Guid> AddAttendeeToEventWithToken(Guid socialEventId, Attendee attendee, string accessToken)
        {
            var userId = CheckToken(accessToken);
            return await AddAttendeeToEvent(socialEventId, attendee,userId);
        }

        public async Task<Guid> DeleteSocialEvent(Guid id)
        {
            var rowsDeleted = await _appUnitOfWork.SocialEventRepository.Delete(id);
            if (rowsDeleted == 0)
            {
                throw new SocialEventException("Social event wasn't deleted");
            }

            _appUnitOfWork.Save();
            return id;
        }

        private void ValidateSocialEvent(SocialEvent socialEvent)
        {
            var result = _validator.Validate(socialEvent);
            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
                throw new SocialEventException(stringBuilder.ToString());
            }
        }

        private Guid CheckToken(string accessToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                throw new TokenException("Invalid token");
            }
            return Guid.Parse(userId);
        }

        public void Dispose()
        {
            _appUnitOfWork.Dispose();
        }
    }
}
