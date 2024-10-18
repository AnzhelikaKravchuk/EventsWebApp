using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface ISocialEventService
    {
        Task<Guid> AddAttendeeToEvent(Guid socialEventId, Attendee attendee, Guid userId, CancellationToken cancellationToken);
        Task<Guid> AddAttendeeToEventWithToken(Guid socialEventId, Attendee attendee, string accessToken, CancellationToken cancellationToken);
        Task<Guid> CreateSocialEvent(SocialEvent socialEvent, CancellationToken cancellationToken);
        Task<Guid> DeleteSocialEvent(Guid id, CancellationToken cancellationToken);
        void Dispose();
        Task<List<Attendee>> GetAttendeesById(Guid id, CancellationToken cancellationToken);
        Task<SocialEvent> GetSocialEventById(Guid id, CancellationToken cancellationToken);
        Task<(SocialEvent, bool)> GetSocialEventByIdWithToken(Guid id, string accessToken, CancellationToken cancellationToken);
        Task<SocialEvent> GetSocialEventByName(string name, CancellationToken cancellationToken);
        Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<Guid> UpdateSocialEvent(SocialEvent socialEvent, CancellationToken cancellationToken);
    }
}