using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
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

        public async Task<List<SocialEvent>> GetAllSocialEvents()
        {
            var socialEvents = await _appUnitOfWork.SocialEventRepository.GetAll();
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

        public async Task<SocialEvent> GetSocialEventByName(string name)
        {
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByName(name);
            if (socialEvent == null)
            {
                throw new Exception("No such social event found");
            }
            return socialEvent;
        }
        public async Task<List<Attendee>> GetAttendeesById(Guid id)
        {
            var attendees = await _appUnitOfWork.SocialEventRepository.GetAllAttendeesByEventId(id);
            return attendees;
        }

        public async Task<Guid> CreateSocialEvent(SocialEvent socialEvent, Guid guid)
        {
            //SocialEvent candidate = await _appUnitOfWork.SocialEventRepository.GetById(socialEvent.Id);

            //if (candidate == null)
            //{
            //    throw new Exception("No candidate found");
            //}

            var user = await _appUnitOfWork.UserRepository.GetById(guid);
            ValidateSocialEvent(socialEvent);
            var attendee = new Attendee("f", "f", "yes@gmail.com", DateTime.MinValue, socialEvent, user);
            socialEvent.ListOfAttendees.Add(attendee);


            await _appUnitOfWork.SocialEventRepository.Add(socialEvent);
            await _appUnitOfWork.AttendeeRepository.Add(attendee);
            _appUnitOfWork.Save();
            return socialEvent.Id;
        }

        public async Task<Guid> UpdateSocialEvent(SocialEvent socialEvent)
        {
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
