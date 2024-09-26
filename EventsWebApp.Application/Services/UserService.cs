using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Models;
using System.Text;

namespace EventsWebApp.Application.Services
{
    public class UserService : IDisposable
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly UserValidator _validator;

        public UserService(IAppUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IMapper mapper, UserValidator validator)
        {
            _appUnitOfWork = appUnitOfWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _appUnitOfWork.UserRepository.GetAll();
            return users.Select(_mapper.Map<UserDto>).ToList();
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            User user = await _appUnitOfWork.UserRepository.GetById(id);
            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            User user = await _appUnitOfWork.UserRepository.GetByEmail(email);
            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return _mapper.Map<UserDto>(user);
        }
        public async Task<string> Register(string email, string password, string username)
        {
            var candidate = await _appUnitOfWork.UserRepository.GetByEmail(email);
            if (candidate != null)
            {
                throw new Exception("User already exists");
            }
            string hashedPassword = _passwordHasher.Generate(password);

            User user = new User(email, hashedPassword, username, "User");
            ValidateUser(user);

            await _appUnitOfWork.UserRepository.Add(user);
            _appUnitOfWork.Save();

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }

        public async Task<string> Login(string email, string password)
        {
            User candidate = await _appUnitOfWork.UserRepository.GetByEmail(email);

            if (candidate == null || !_passwordHasher.Verify(password, candidate.PasswordHash))
            {
                throw new Exception("No candidate found");
            }

            var token = _jwtProvider.GenerateToken(candidate);

            return token;
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
