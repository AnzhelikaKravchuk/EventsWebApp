using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface ISocialEventRepository : IBaseRepository<SocialEvent>
    {
        Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<SocialEvent> GetByName(string name, CancellationToken cancellationToken);
        Task<SocialEvent> GetByIdTracking(Guid id, CancellationToken cancellationToken);
        Task<(Attendee?, SocialEvent)> GetAttendeeWithEventByEmail(Guid socialEventId, string attendeeEmail, CancellationToken cancellationToken);
    }
}