using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<User> GetByIdTracking(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User> GetByEmailTracking(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User> GetByName(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            User user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == name);

            return user;
        }
    }
}
