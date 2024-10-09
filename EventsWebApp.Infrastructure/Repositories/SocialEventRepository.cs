using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces.Repositories;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class SocialEventRepository : ISocialEventRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SocialEventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SocialEvent> GetById(Guid id)
        {
            var socialEvent = await _dbContext.SocialEvents.Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.Id == id);

            return socialEvent;
        }

        public async Task<SocialEvent> GetByName(string name)
        {
            var socialEvent = await _dbContext.SocialEvents.Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.EventName.Contains(name));

            return socialEvent;
        }

        public async Task<PaginatedList<SocialEvent>> GetSocialEvents(AppliedFilters filters, int pageIndex, int pageSize)
        {
            var allEvents = await _dbContext.SocialEvents
                                                    .Include(s => s.ListOfAttendees)
                                                    .AsNoTracking()
                                                    .Where(s => s.EventName.Contains(filters.Name ?? ""))
                                                    .Where(s => filters.Date.IsNullOrEmpty() || s.Date == DateTime.Parse(filters.Date).Date)
                                                    .Where(s => filters.Category.IsNullOrEmpty() || s.Category == (E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), filters.Category))
                                                    .Where(s => s.Place.Contains(filters.Place ?? ""))
                                                    .OrderBy(s => s.EventName).ToListAsync();
            
            var onPageEvents = allEvents.Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(allEvents.Count / (double)pageSize);

            return new PaginatedList<SocialEvent>(onPageEvents, pageIndex, totalPages);
        }


        public async Task<Attendee> GetAttendeeByEmail(Guid socialEventId, string attendeeEmail)
        {
            var socialEvent = await _dbContext.SocialEvents.Include(s => s.ListOfAttendees).FirstOrDefaultAsync(x => x.Id == socialEventId);
            if (socialEvent == null)
            {
                throw new SocialEventException("No event was found");
            }
            var attendeeList = socialEvent.ListOfAttendees;
            if (attendeeList == null)
            {
                throw new SocialEventException("Attendee list is empty");
            }
            var attendee = attendeeList.FirstOrDefault(a => a.Email == attendeeEmail);

            return attendee;
        }

        public async Task<Guid> Add(SocialEvent socialEvent)
        {
            var entity = await _dbContext.SocialEvents.AddAsync(socialEvent);

            return entity.Entity.Id;
        }

        public async Task<Guid> Update(SocialEvent socialEvent)
        {
            await _dbContext.SocialEvents
                .Where(x => x.Id == socialEvent.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(s => s.EventName, s => socialEvent.EventName)
                    .SetProperty(s => s.Description, s => socialEvent.Description)
                    .SetProperty(s => s.Date, s => socialEvent.Date)
                    .SetProperty(s => s.Place, s => socialEvent.Place)
                    .SetProperty(s => s.MaxAttendee, s => socialEvent.MaxAttendee)
                    .SetProperty(s => s.Category, s => socialEvent.Category)
                    .SetProperty(s => s.Image, s => socialEvent.Image ?? "")
                    );
            return socialEvent.Id;
        }

        public async Task<int> Delete(Guid id)
        {
            int rowsDeleted = await _dbContext.SocialEvents.Where(x => x.Id == id).ExecuteDeleteAsync();

            return rowsDeleted;
        }
    }
}
