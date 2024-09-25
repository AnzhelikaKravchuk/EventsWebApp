using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users.Select(u => UserMapper.Map(u)).ToList();
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            User user = await _userRepository.GetById(id);
            return UserMapper.Map(user);
        }
        public async Task Register(string email, string password, string username)
        {
            string hashedPassword = _passwordHasher.Generate(password);

            User user = new User(Guid.NewGuid(), email, hashedPassword, username, "User");
            await _userRepository.Add(user);
        }
        public async Task<string> Login(string email, string password)
        {
            User candidate = await _userRepository.GetByEmail(email);

            if (candidate == null || !_passwordHasher.Verify(password, candidate.PasswordHash))
            {
                throw new Exception("No candidate found");
            }

            var token = _jwtProvider.GenerateToken(candidate);

            return token;
        }

        public async Task<Guid> UpdateUser(Guid id, string email, string password, string username)
        {
            return await _userRepository.Update(id, email, password, username);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
