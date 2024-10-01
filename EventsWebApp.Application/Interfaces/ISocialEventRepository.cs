using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Application.Interfaces
{
    public interface ISocialEventRepository
    {
        Task<Guid> Add(SocialEvent socialEvent);
        Task<Guid> AddAttendee(Guid socialEventId, Attendee attendee);
        Task<Guid> Delete(Guid id);
        Task<SocialEvent> GetById(Guid id);
        Task<List<Attendee>> GetAllAttendeesByEventId(Guid id);
        Task<Attendee> GetAttendeeById(Guid socialEventId, Guid attendeeId);
        Task<Guid> Update(SocialEvent socialEvent);
        Task<Attendee> GetAttendeeByEmail(Guid socialEventId, string attendeeEmail);
        Task<PaginatedList<SocialEvent>> GetSocialEvents(int pageIndex, int pageSize);
        Task<List<SocialEvent>> GetByDate(DateTime date);
        Task<List<SocialEvent>> GetByCategory(E_SocialEventCategory category);
        Task<List<SocialEvent>> GetByPlace(string place);
        Task<List<SocialEvent>> GetByName(string name);
    }
}