using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Models;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class AttendeeService : IDisposable, IAttendeeService
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
            if (userId == null)
            {
                throw new UserException("No user id found");
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

        public async Task<Guid> AddAttendee(Attendee attendee, SocialEvent socialEvent, Guid userId)
        {
            User user = await _appUnitOfWork.UserRepository.GetByIdTracking(userId);

            if (user == null)
            {
                throw new UserException("No user was found");
            }
            attendee.SocialEvent = socialEvent;
            attendee.User = user;

            ValidateAttendee(attendee);

            socialEvent.ListOfAttendees.Add(attendee);
            var resultId = await _appUnitOfWork.AttendeeRepository.Add(attendee);
            _appUnitOfWork.Save();
            return resultId;
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

            if (attendee == null)
            {
                throw new AttendeeException("No attendee found");
            }

            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(attendee.SocialEvent.Id);

            //socialEvent.ListOfAttendees.Remove(attendee);
            await _appUnitOfWork.AttendeeRepository.Delete(id);

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
                throw new AttendeeException(stringBuilder.ToString());
            }
        }

        public void Dispose()
        {
            _appUnitOfWork.Dispose();
        }
    }
}
