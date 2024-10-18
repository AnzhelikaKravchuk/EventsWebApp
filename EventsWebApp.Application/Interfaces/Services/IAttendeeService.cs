using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface IAttendeeService
    {
        Task<Guid> AddAttendee(Attendee attendee, SocialEvent socialEvent, Guid userId);
        Task<Guid> DeleteAttendee(Guid id);
        void Dispose();
        Task<List<Attendee>> GetAllAttendees();
        Task<List<Attendee>> GetAllAttendeesByUserId(Guid userId);
        Task<Attendee> GetAttendeeById(Guid id);
        Task<List<Attendee>> GetAttendeesByToken(string accessToken);
        Task<Guid> UpdateAttendee(Attendee attendee);
    }
}