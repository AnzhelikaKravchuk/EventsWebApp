using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
namespace EventsWebApp.Application.UseCases.Users.Queries
{
    public record GetUserByEmailQuery(string Email) : IQuery<UserDto>;
}
