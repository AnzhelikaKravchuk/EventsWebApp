using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<Guid> Delete(Guid id);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAll();
        Task<Guid> Update(User user);
    }
}
