using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Application.Interfaces.Repositories
{
    public interface ISocialEventRepository
    {
        Task<Guid> Add(SocialEvent socialEvent);
        //Task<Guid> AddAttendee(Guid socialEventId, Attendee attendee);
        Task<int> Delete(Guid id);
        Task<SocialEvent> GetById(Guid id);
        //Task<List<Attendee>> GetAllAttendeesByEventId(Guid id);
        //Task<Attendee> GetAttendeeById(Guid socialEventId, Guid attendeeId);
        Task<Guid> Update(SocialEvent socialEvent);
        Task<Attendee> GetAttendeeByEmail(Guid socialEventId, string attendeeEmail);
        Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize);
        Task<SocialEvent> GetByName(string name);
    }
}