using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
namespace EventsWebApp.Application.Users.Queries.GetUserByEmailQuery
{
    public record GetUserByEmailQuery(string Email) : IQuery<UserDto>;
}
