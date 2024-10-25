using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface ISocialEventRepository : IBaseRepository<SocialEvent>
    {
        public Task<List<SocialEvent>> GetAllWithInclude(CancellationToken cancellationToken);
        Task<(List<SocialEvent>, int)> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<SocialEvent> GetByName(string name, CancellationToken cancellationToken);
        Task<SocialEvent> GetByIdWithInclude(Guid id, CancellationToken cancellationToken);
        Task<SocialEvent> GetByIdWithIncludeTracking(Guid id, CancellationToken cancellationToken);
        Task<(Attendee?, SocialEvent?)> GetAttendeeWithEventByEmail(Guid socialEventId, string attendeeEmail, CancellationToken cancellationToken);
    }
}