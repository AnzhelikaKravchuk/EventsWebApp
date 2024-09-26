using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            var socialEvent = await _dbContext.SocialEvents.FirstOrDefaultAsync(x => x.Id == id);

            return socialEvent;
        }

        public async Task<SocialEvent> GetByName(string name)
        {
            var socialEvent = await _dbContext.SocialEvents.FirstOrDefaultAsync(x => x.Name == name);

            return socialEvent;
        }

        public async Task<List<SocialEvent>> GetAll()
        {
            return await _dbContext.SocialEvents.AsNoTracking().ToListAsync();
        }

        public async Task<List<Attendee>> GetAllAttendeesByEventId(Guid id)
        {
            var socialEvent = await _dbContext.SocialEvents.FirstOrDefaultAsync(x => x.Id == id);
            if (socialEvent == null)
            {
                throw new Exception("No event was found");
            }
            return socialEvent.Attendees;
        }

        //REFACTOR
        public async Task<Attendee> GetAttendeeById(Guid socialEventId, Guid attendeeId)
        {
            var socialEvent = await _dbContext.SocialEvents.FirstOrDefaultAsync(x => x.Id == socialEventId);
            if (socialEvent == null)
            {
                throw new Exception("No event was found");
            }
            var attendeeList = socialEvent.Attendees;
            if(attendeeList == null)
            {
                throw new Exception("Attendee list is empty");
            }
            var attendee = attendeeList.FirstOrDefault(a => a.Id == attendeeId);

            return attendee;
        }

        public async Task<Guid> Add(SocialEvent socialEvent)
        {
            await _dbContext.SocialEvents.AddAsync(socialEvent);

            return socialEvent.Id;
        }

        public async Task<Guid> Update(SocialEvent socialEvent)
        {
            await _dbContext.SocialEvents
                .Where(x => x.Id == socialEvent.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(s => s.Name, s => socialEvent.Name)
                    .SetProperty(s => s.Description, s => socialEvent.Description)
                    .SetProperty(s => s.Date, s => socialEvent.Date)
                    .SetProperty(s => s.Place, s => socialEvent.Place)
                    .SetProperty(s => s.MaxAttendee, s => socialEvent.MaxAttendee)
                    .SetProperty(s => s.Category, s => socialEvent.Category)
                    .SetProperty(s => s.Image, s => socialEvent.Image)
                    );
            return socialEvent.Id;
        }

        public async Task<Guid> AddAttendee(Guid socialEventId, Attendee attendee)
        {
            var socialEvent = await _dbContext.SocialEvents.FirstOrDefaultAsync(x => x.Id == socialEventId);

            if (socialEvent == null)
            {
                throw new Exception("No event was found");
            }
            var attendeesList = socialEvent.Attendees;
            if (attendeesList == null)
            {
                attendeesList = new List<Attendee>();
            }
            attendeesList.Add(attendee);

            return socialEventId;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.SocialEvents.Where(x => x.Id == id).ExecuteDeleteAsync();

            return id;
        }
    }
}
