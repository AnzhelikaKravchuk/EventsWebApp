using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Users.Queries
{
    public record GetUserByIdQuery : IdRequest, IQuery<UserDto>;
}
