using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class SocialEventService : IDisposable
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly SocialEventValidator _validator;
        public SocialEventService(IAppUnitOfWork appUnitOfWork, SocialEventValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _validator = validator;
        }

        public async Task<PaginatedList<SocialEvent>> GetAllSocialEvents(int pageIndex = 1, int pageSize = 10)
        {
            var socialEvents = await _appUnitOfWork.SocialEventRepository.GetSocialEvents(pageIndex, pageSize);
            return socialEvents;
        }

        public async Task<SocialEvent> GetSocialEventById(Guid id)
        {
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(id);
            if (socialEvent == null)
            {
                throw new Exception("No such social event found");
            }
            return socialEvent;
        }

        public async Task<List<SocialEvent>> GetSocialEventsByName(string name)
        {
            return await _appUnitOfWork.SocialEventRepository.GetByName(name);
        }

        public async Task<List<SocialEvent>> GetSocialEventsByDate(DateTime date)
        {
            return await _appUnitOfWork.SocialEventRepository.GetByDate(date);
        }

        public async Task<List<SocialEvent>> GetSocialEventsByCategory(E_SocialEventCategory category)
        {
            return await _appUnitOfWork.SocialEventRepository.GetByCategory(category);
        }

        public async Task<List<SocialEvent>> GetSocialEventsByPlace(string place)
        {
            return await _appUnitOfWork.SocialEventRepository.GetByPlace(place);
        }

        public async Task<List<Attendee>> GetAttendeesById(Guid id)
        {
            var attendees = await _appUnitOfWork.SocialEventRepository.GetAllAttendeesByEventId(id);
            return attendees;
        }

        public async Task<Guid> CreateSocialEvent(SocialEvent socialEvent)
        {
            //var user = await _appUnitOfWork.UserRepository.GetById(userId);
            //ValidateSocialEvent(socialEvent);
            //socialEvent.ListOfAttendees.Add(attendee);


            //await _appUnitOfWork.SocialEventRepository.Add(socialEvent);
            //await _appUnitOfWork.AttendeeRepository.Add(attendee);
            //_appUnitOfWork.Save();
            //return socialEvent.Id;

            ValidateSocialEvent(socialEvent);

            await _appUnitOfWork.SocialEventRepository.Add(socialEvent);
            _appUnitOfWork.Save();
            return socialEvent.Id;
        }

        public async Task<Guid> UpdateSocialEvent(SocialEvent socialEvent)
        {
            var candidate = await _appUnitOfWork.SocialEventRepository.GetById(socialEvent.Id);
            if (candidate == null)
            {
                throw new Exception("No candidate found");
            }
            ValidateSocialEvent(socialEvent);

            var id = await _appUnitOfWork.SocialEventRepository.Update(socialEvent);

            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> AddAttendeeToEvent(Guid socialEventId, Attendee attendee)
        {
            var id = await _appUnitOfWork.SocialEventRepository.AddAttendee(socialEventId, attendee);

            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> DeleteSocialEvent(Guid id)
        {
            var deletedId = await _appUnitOfWork.SocialEventRepository.Delete(id);

            _appUnitOfWork.Save();
            return deletedId;
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
                throw new Exception(stringBuilder.ToString());
            }
        }

        public void Dispose()
        {
            _appUnitOfWork.Dispose();
        }
    }
}
