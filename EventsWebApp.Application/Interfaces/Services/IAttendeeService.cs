using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface IAttendeeService
    {
        Task<Guid> AddAttendee(Attendee attendee, SocialEvent socialEvent, Guid userId, CancellationToken cancellationToken);
        Task<Guid> DeleteAttendee(Guid id, CancellationToken cancellationToken);
        void Dispose();
        Task<List<Attendee>> GetAllAttendees(CancellationToken cancellationToken);
        Task<List<Attendee>> GetAllAttendeesByUserId(Guid userId, CancellationToken cancellationToken);
        Task<Attendee> GetAttendeeById(Guid id, CancellationToken cancellationToken);
        Task<List<Attendee>> GetAttendeesByToken(string accessToken, CancellationToken cancellationToken);
        Task<Guid> UpdateAttendee(Attendee attendee, CancellationToken cancellationToken);
    }
}