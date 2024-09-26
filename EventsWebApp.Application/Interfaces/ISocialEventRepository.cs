using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface ISocialEventRepository
    {
        Task<Guid> Add(SocialEvent socialEvent);
        Task<Guid> AddAttendee(Guid socialEventId, Attendee attendee);
        Task<Guid> Delete(Guid id);
        Task<List<SocialEvent>> GetAll();
        Task<SocialEvent> GetById(Guid id);
        Task<SocialEvent> GetByName(string name);
        Task<List<Attendee>> GetAllAttendeesByEventId(Guid id);
        Task<Attendee> GetAttendeeById(Guid socialEventId, Guid attendeeId);
        Task<Guid> Update(SocialEvent socialEvent);
    }
}