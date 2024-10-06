using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using System.Security.Claims;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class AttendeeService : IDisposable
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly AttendeeValidator _validator;
        public AttendeeService(IAppUnitOfWork appUnitOfWork, AttendeeValidator validator, IJwtProvider jwtProvider)
        {
            _appUnitOfWork = appUnitOfWork;
            _validator = validator;
            _jwtProvider = jwtProvider;
        }

        public async Task<Attendee> GetAttendeeById(Guid id)
        {
            return await _appUnitOfWork.AttendeeRepository.GetById(id);
        }

        public async Task<List<Attendee>> GetAttendeesByToken(string accessToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if(userId == null)
            {
                throw new Exception("No user id found");
            }

            return await _appUnitOfWork.AttendeeRepository.GetAllByUserId(Guid.Parse(userId));
        }

        public async Task<List<Attendee>> GetAllAttendeesByUserId(Guid userId)
        {
            return await _appUnitOfWork.AttendeeRepository.GetAllByUserId(userId);
        }

        public async Task<List<Attendee>> GetAllAttendees()
        {
            return await _appUnitOfWork.AttendeeRepository.GetAll();
            
        }

        public async Task<Guid> AddAttendeeToEvent(Attendee attendee, Guid socialEventId, Guid userId)
        {
            Attendee candidate = await _appUnitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEventId, attendee.Email);
            SocialEvent socialEvent= await _appUnitOfWork.SocialEventRepository.GetById(socialEventId);
            User user = await _appUnitOfWork.UserRepository.GetById(userId);

            if (candidate != null)
            {
                throw new Exception("This attendee already in the list");
            }
            attendee.SocialEvent = socialEvent;
            attendee.User = user;

            ValidateAttendee(attendee);

            await _appUnitOfWork.SocialEventRepository.AddAttendee(socialEventId, attendee);
            await _appUnitOfWork.AttendeeRepository.Add(attendee);
            _appUnitOfWork.Save();
            return attendee.Id;
        }

        public async Task<Guid> AddAttendeeToEventWithToken(Attendee attendee, Guid socialEventId, string accessToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                throw new Exception("No user id found");
            }
            return await AddAttendeeToEvent(attendee, socialEventId, Guid.Parse(userId));
        }

        public async Task<Guid> UpdateAttendee(Attendee attendee)
        {
            ValidateAttendee(attendee);

            var id = await _appUnitOfWork.AttendeeRepository.Update(attendee);

            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> DeleteAttendee(Guid id)
        {
            var attendee = await _appUnitOfWork.AttendeeRepository.GetById(id);

            if(attendee == null)
            {
                throw new Exception("No attendee found");
            }

            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(attendee.SocialEvent.Id);

            socialEvent.ListOfAttendees.Remove(attendee);
            await _appUnitOfWork.SocialEventRepository.Update(socialEvent);
            //var deletedId = await _appUnitOfWork.AttendeeRepository.Delete(id);

            _appUnitOfWork.Save();
            return id;
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
