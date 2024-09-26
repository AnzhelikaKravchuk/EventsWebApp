using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AttendeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Attendee> GetById(Guid id)
        {
            var attendee = await _dbContext.Attendees.FirstOrDefaultAsync(x => x.Id == id);

            return attendee;
        }

        public async Task<List<Attendee>> GetAll()
        {
            return await _dbContext.Attendees.AsNoTracking().ToListAsync();
        }

        public async Task<Guid> Add(Attendee attendee)
        {
            await _dbContext.Attendees.AddAsync(attendee);

            return attendee.Id;
        }

        public async Task<Guid> Update(Attendee attendee)
        {
            await _dbContext.Attendees
                .Where(x => x.Id == attendee.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(s => s.Name, s => attendee.Name)
                    .SetProperty(s => s.Surname, s => attendee.Surname)
                    .SetProperty(s => s.DateOfBirth, s => attendee.DateOfBirth)
                    .SetProperty(s => s.Email, s => attendee.Email)
                    );
            return attendee.Id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Attendees.Where(x => x.Id == id).ExecuteDeleteAsync();

            return id;
        }
    }
}
