using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> DeleteUser(Guid id);
        void Dispose();
        Task<List<User>> GetAllUsers();
        string GetRoleByToken(string accessToken);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task<(string, string)> Login(string email, string password);
        Task<string> RefreshToken(string accessToken, string refreshToken);
        Task<(string, string)> Register(string email, string password, string username);
        Task<Guid> UpdateUser(User user);
    }
}