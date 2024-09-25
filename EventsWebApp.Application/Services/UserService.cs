using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Services
{
    public class UserService
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public UserService(IAppUnitOfWork appUnitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _appUnitOfWork.UserRepository.GetAll();
            return users.Select(_mapper.Map<UserDto>).ToList();
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            User user = await _appUnitOfWork.UserRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task Register(string email, string password, string username)
        {
            var candidate = await _appUnitOfWork.UserRepository.GetByEmail(email);
            if(candidate != null)
            {
                throw new Exception("User already exists");
            }
            string hashedPassword = _passwordHasher.Generate(password);

            User user = new User(Guid.NewGuid(), email, hashedPassword, username, "User");
            await _appUnitOfWork.UserRepository.Add(user);
            _appUnitOfWork.Save();
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

        public async Task<Guid> UpdateUser(Guid id, string email, string password, string username)
        {
            var userId = await _appUnitOfWork.UserRepository.Update(id, email, password, username);

            _appUnitOfWork.Save();
            return userId;
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            var userId = await _appUnitOfWork.UserRepository.Delete(id);

            _appUnitOfWork.Save();
            return userId;
        }
        protected void Dispose(bool disposing)
        {
            _appUnitOfWork.Dispose();
        }
    }
}
