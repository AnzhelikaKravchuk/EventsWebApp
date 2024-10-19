using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Queries.GetUserByIdQuery
{
    public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;
}
