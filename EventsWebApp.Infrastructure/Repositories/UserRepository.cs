using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetById(Guid id)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            
            return user;
        }

        public async Task<User> GetByIdTracking(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User> GetByName(string name)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == name);

            return user;
        }
        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<Guid> Add(User user)
        { 
            var addedUser = await _dbContext.Users.AddAsync(user);

            return addedUser.Entity.Id;
        }

        public async Task<Guid> Update(User user)
        {
            await _dbContext.Users
                .Where(x => x.Id == user.Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(u => u.Email, u => user.Email)
                    .SetProperty(u => u.PasswordHash, u => user.PasswordHash)
                    .SetProperty(u => u.Username, u => user.Username)
                    );
            return user.Id;
        }

        public async Task<int> Delete(Guid id)
        {
            int deletedRows = await _dbContext.Users.Where(x => x.Id == id).ExecuteDeleteAsync();

            return deletedRows;
        }
    }
}
