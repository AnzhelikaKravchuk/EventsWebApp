using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> DeleteUser(Guid id, CancellationToken cancellationToken);
        void Dispose();
        Task<List<User>> GetAllUsers(CancellationToken cancellationToken);
        string? GetRoleByToken(string accessToken, CancellationToken cancellationToken);
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetUserById(Guid id, CancellationToken cancellationToken);
        Task<(string, string)> Login(string email, string password, CancellationToken cancellationToken);
        Task<string> RefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken);
        Task<(string, string)> Register(string email, string password, string username, CancellationToken cancellationToken);
        Task<Guid> UpdateUser(User user, CancellationToken cancellationToken);
    }
}