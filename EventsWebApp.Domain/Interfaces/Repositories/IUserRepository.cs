using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetById(Guid id, CancellationToken cancellationToken);
        Task<List<User>> GetAll(CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByEmailTracking(string email, CancellationToken cancellationToken);
        Task<User> GetByIdTracking(Guid id, CancellationToken cancellationToken);
        Task<User> GetByName(string name, CancellationToken cancellationToken);
    }
}
