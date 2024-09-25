using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application
{
    public class UserMapper
    {
        //TODO: Change
        public static UserDto Map(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
