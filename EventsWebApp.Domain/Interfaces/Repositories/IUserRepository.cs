using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByIdTracking(Guid id, CancellationToken cancellationToken);
        Task<User> GetByName(string name, CancellationToken cancellationToken);
    }
}
