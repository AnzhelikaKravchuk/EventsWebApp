using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Queries.GetAllUsersQuery
{
    public record GetAllUsersQuery : IQuery<List<UserDto>>;
}
