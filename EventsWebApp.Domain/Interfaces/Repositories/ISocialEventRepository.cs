using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface ISocialEventRepository : IBaseRepository<SocialEvent>
    {
        Task<Attendee> GetAttendeeByEmail(Guid socialEventId, string attendeeEmail);
        Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize);
        Task<SocialEvent> GetByName(string name);
        Task<SocialEvent> GetByIdTracking(Guid id);
    }
}