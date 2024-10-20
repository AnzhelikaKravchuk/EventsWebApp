using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Queries
{
    public record GetAllUsersQuery : IQuery<List<UserDto>>;
}
