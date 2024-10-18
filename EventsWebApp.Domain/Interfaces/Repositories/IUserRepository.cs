using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByIdTracking(Guid id);
        Task<User> GetByName(string name);
    }
}
