using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EventsWebApp.Domain.Filters;
using System.Linq.Expressions;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class SocialEventRepository : GenericRepository<SocialEvent>,ISocialEventRepository
    {
        private Expression<Func<SocialEvent, object>>[] includes = [x => x.ListOfAttendees];
        public SocialEventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<SocialEvent> GetByIdWithInclude(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.AsNoTracking().Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SocialEvent> GetByIdWithIncludeTracking(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SocialEvent> GetByName(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _dbSet.AsNoTracking().Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.EventName.Contains(name));

            return socialEvent;
        }

        public async Task<List<SocialEvent>> GetAllWithInclude(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.AsNoTracking().Include(s => s.ListOfAttendees).ToListAsync();
        }

        public async Task<(List<SocialEvent>, int)> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var allEvents = _dbSet.Include(s => s.ListOfAttendees)
                                                    .AsNoTracking()
                                                    .Where(s => s.EventName.Contains(filters.Name ?? ""))
                                                    .Where(s => filters.Date.IsNullOrEmpty() || s.Date == DateTime.Parse(filters.Date).Date)
                                                    .Where(s => filters.Category.IsNullOrEmpty() || s.Category == (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), filters.Category))
                                                    .Where(s => s.Place.Contains(filters.Place ?? ""))
                                                    .OrderBy(s => s.EventName);

            cancellationToken.ThrowIfCancellationRequested();
            List<SocialEvent> onPageEvents = await allEvents.Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize).ToListAsync();

            int totalPages = (int)Math.Ceiling(allEvents.Count() / (double)pageSize);
            return (onPageEvents, totalPages);
        }

        public async Task<(Attendee?, SocialEvent?)> GetAttendeeWithEventByEmail(Guid socialEventId, string attendeeEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent? socialEvent = await _dbSet.Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.Id == socialEventId);

            List<Attendee>? attendeeList = socialEvent?.ListOfAttendees;
            cancellationToken.ThrowIfCancellationRequested();
            Attendee? attendee = attendeeList?.FirstOrDefault(a => a?.Email == attendeeEmail);

            return (attendee, socialEvent);
        }
    }
}
