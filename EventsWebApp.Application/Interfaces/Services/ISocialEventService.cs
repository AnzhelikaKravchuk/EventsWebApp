using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface ISocialEventService
    {
        Task<Guid> AddAttendeeToEvent(Guid socialEventId, Attendee attendee, Guid userId);
        Task<Guid> AddAttendeeToEventWithToken(Guid socialEventId, Attendee attendee, string accessToken);
        Task<Guid> CreateSocialEvent(SocialEvent socialEvent);
        Task<Guid> DeleteSocialEvent(Guid id);
        void Dispose();
        Task<List<Attendee>> GetAttendeesById(Guid id);
        Task<SocialEvent> GetSocialEventById(Guid id);
        Task<(SocialEvent, bool)> GetSocialEventByIdWithToken(Guid id, string accessToken);
        Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex = 1, int pageSize = 10);
        Task<Guid> UpdateSocialEvent(SocialEvent socialEvent);
    }
}