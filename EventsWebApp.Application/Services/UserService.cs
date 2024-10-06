using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using System.Security.Claims;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class UserService : IDisposable
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly UserValidator _validator;

        public UserService(IAppUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, UserValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _appUnitOfWork.UserRepository.GetAll();
            return users;
        }

        public async Task<User> GetUserById(Guid id)
        {
            User user = await _appUnitOfWork.UserRepository.GetById(id);
            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _appUnitOfWork.UserRepository.GetByEmail(email);
            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return user;
        }
        public async Task<(string, string)> Register(string email, string password, string username)
        {
            var candidate = await _appUnitOfWork.UserRepository.GetByEmail(email);
            if (candidate != null)
            {
                throw new Exception("User already exists");
            }
            string hashedPassword = _passwordHasher.Generate(password);

            User user = new User(email, hashedPassword, username, "User");
            ValidateUser(user);

            var addedUser =await _appUnitOfWork.UserRepository.Add(user);

            var (accessToken, refreshToken) = _jwtProvider.CreateTokens(addedUser);
            _appUnitOfWork.Save();

            return (accessToken, refreshToken);
        }

        public async Task<(string,string)> Login(string email, string password)
        {
            User candidate = await _appUnitOfWork.UserRepository.GetByEmail(email);

            if (candidate == null || !_passwordHasher.Verify(password, candidate.PasswordHash))
            {
                throw new Exception("No candidate found");
            }


            var (accessToken, refreshToken) = _jwtProvider.CreateTokens(candidate);

            _appUnitOfWork.Save();
            return (accessToken, refreshToken);
        }

        public async Task<Guid> UpdateUser(User user)
        {
            ValidateUser(user);

            var userId = await _appUnitOfWork.UserRepository.Update(user);

            _appUnitOfWork.Save();
            return userId;
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            var userId = await _appUnitOfWork.UserRepository.Delete(id);

            _appUnitOfWork.Save();
            return userId;
        }

        public string GetRoleByToken(string accessToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
        }

        public async Task<string> RefreshToken(string accessToken, string refreshToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            var user = await _appUnitOfWork.UserRepository.GetByEmail(
            principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);

            if(user == null || user.RefreshToken != refreshToken || user.ExpiresRefreshToken <= DateTime.UtcNow) {
                throw new Exception("Token invalid");
            }

            accessToken  = _jwtProvider.GenerateAccessToken(user);
            _appUnitOfWork.Save();
            return accessToken;
        }

        private void ValidateUser(User user)
        {
            var result = _validator.Validate(user);
            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
                throw new Exception(stringBuilder.ToString());
            }
        }
        public void Dispose()
        {
            _appUnitOfWork.Dispose();
        }
    }
}
