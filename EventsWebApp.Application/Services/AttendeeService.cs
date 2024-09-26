using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class AttendeeService : IDisposable
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly AttendeeValidator _validator;
        public AttendeeService(IAppUnitOfWork appUnitOfWork, AttendeeValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _validator = validator;
        }

        public async Task<Attendee> GetAttendeeById(Guid id)
        {
            return await _appUnitOfWork.AttendeeRepository.GetById(id);
        }

        public async Task<List<SocialEvent>> GetAllAttendees()
        {
            var attendees = await _appUnitOfWork.SocialEventRepository.GetAll();
            return attendees;
        }

        public async Task<Guid> AddAttendeeToEvent(Guid socialEventId, Attendee attendee)
        {
            Attendee candidate = await _appUnitOfWork.SocialEventRepository.GetAttendeeById(socialEventId, attendee.Id);

            if (candidate == null)
            {
                throw new Exception("No candidate found");
            }

            ValidateAttendee(attendee);

            await _appUnitOfWork.SocialEventRepository.AddAttendee(socialEventId, attendee);
            await _appUnitOfWork.AttendeeRepository.Add(candidate);
            _appUnitOfWork.Save();
            return attendee.Id;
        }

        public async Task<Guid> UpdateAttendee(Attendee attendee)
        {
            ValidateAttendee(attendee);

            var id = await _appUnitOfWork.AttendeeRepository.Update(attendee);

            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> DeleteSocialEvent(Guid id)
        {
            var deletedId = await _appUnitOfWork.AttendeeRepository.Delete(id);

            _appUnitOfWork.Save();
            return deletedId;
        }

        private void ValidateAttendee(Attendee attendee)
        {
            var result = _validator.Validate(attendee);
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
