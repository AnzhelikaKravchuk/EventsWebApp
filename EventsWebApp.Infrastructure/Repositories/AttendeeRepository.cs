using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class AttendeeRepository : GenericRepository<Attendee>, IAttendeeRepository
    {
        public AttendeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Attendee>> GetAllWithInclude(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.AsNoTracking().Include(s => s.User).Include(s => s.SocialEvent).ToListAsync();
        }

        public async Task<Attendee> GetByIdWithInclude(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.AsNoTracking().Include(s => s.User).Include(s => s.SocialEvent).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Attendee>> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<Attendee> attendees = await _dbSet.AsNoTracking().Include(s => s.User).Include(s => s.SocialEvent).Where(x => x.User.Id == userId).ToListAsync();

            return attendees;
        }
    }
}
