using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure.Entity;
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
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return user.ToDomainUser();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return user.ToDomainUser();
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.AsNoTracking().Select(entity => entity.ToDomainUser()).ToListAsync();
        }

        public async Task<Guid> Add(User user)
        {
            UserEntity userEntity = new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Username = user.Username,
                Role = user.Role,
            };
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Update(Guid id, string email, string password, string username)
        {
            await _dbContext.Users
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.PasswordHash, u => password)
                    .SetProperty(u => u.Username, u => username)
                    );
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Users.Where(x => x.Id == id).ExecuteDeleteAsync();

            return id;
        }
    }
}
