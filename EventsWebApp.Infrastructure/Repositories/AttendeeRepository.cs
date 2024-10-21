using EventsWebApp.Domain.Interfaces.Repositories;
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
        public async Task<Attendee> GetById(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Attendee attendee = await _dbContext.Attendees.Include(s => s.User).Include(s => s.SocialEvent).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return attendee;
        }

        public async Task<List<Attendee>> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<Attendee> attendees = await _dbContext.Attendees.Include(s => s.User).Include(s => s.SocialEvent).Where(x => x.User.Id == userId).AsNoTracking().ToListAsync();

            return attendees;
        }

        public async Task<List<Attendee>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbContext.Attendees.Include(s => s.User).Include(s => s.SocialEvent).AsNoTracking().ToListAsync();
        }

        public async Task<Guid> Add(Attendee attendee, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _dbContext.Attendees.AddAsync(attendee);

            return result.Entity.Id;
        }

        public async Task<Guid> Update(Attendee attendee, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
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
        public async Task<int> Delete(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int rowsDeleted = await _dbContext.Attendees.Where(x => x.Id == id).ExecuteDeleteAsync();

            return rowsDeleted;
        }
    }
}
