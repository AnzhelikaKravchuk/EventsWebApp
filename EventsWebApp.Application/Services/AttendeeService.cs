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

        public async Task<Attendee> GetAttendeeById(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _appUnitOfWork.AttendeeRepository.GetById(id, cancellationToken);
        }

        public async Task<List<Attendee>> GetAttendeesByToken(string accessToken, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            cancellationToken.ThrowIfCancellationRequested();
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                throw new UserException("No user id found");
            }

            return await _appUnitOfWork.AttendeeRepository.GetAllByUserId(Guid.Parse(userId), cancellationToken);
        }

        public async Task<List<Attendee>> GetAllAttendeesByUserId(Guid userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _appUnitOfWork.AttendeeRepository.GetAllByUserId(userId, cancellationToken);
        }

        public async Task<List<Attendee>> GetAllAttendees(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _appUnitOfWork.AttendeeRepository.GetAll(cancellationToken);
        }

        public async Task<Guid> AddAttendee(Attendee attendee, SocialEvent socialEvent, Guid userId, CancellationToken cancellationToken)
        {
            User user = await _appUnitOfWork.UserRepository.GetByIdTracking(userId, cancellationToken);

            if (user == null)
            {
                throw new UserException("No user was found");
            }
            attendee.SocialEvent = socialEvent;
            attendee.User = user;

            cancellationToken.ThrowIfCancellationRequested();
            ValidateAttendee(attendee);

            socialEvent.ListOfAttendees.Add(attendee);
            var resultId = await _appUnitOfWork.AttendeeRepository.Add(attendee, cancellationToken);
            _appUnitOfWork.Save();
            return resultId;
        }

        public async Task<Guid> UpdateAttendee(Attendee attendee, CancellationToken cancellationToken)
        {
            ValidateAttendee(attendee);

            cancellationToken.ThrowIfCancellationRequested();
            var id = await _appUnitOfWork.AttendeeRepository.Update(attendee, cancellationToken);

            _appUnitOfWork.Save();
            return id;
        }

        public async Task<Guid> DeleteAttendee(Guid id, CancellationToken cancellationToken)
        {
            var attendee = await _appUnitOfWork.AttendeeRepository.GetById(id, cancellationToken);

            if (attendee == null)
            {
                throw new AttendeeException("No attendee found");
            }

            cancellationToken.ThrowIfCancellationRequested();

            await _appUnitOfWork.AttendeeRepository.Delete(id, cancellationToken);

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
