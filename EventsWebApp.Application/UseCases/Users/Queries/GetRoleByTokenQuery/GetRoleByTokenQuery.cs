using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.Users.Queries
{
    public record GetRoleByTokenQuery(string AccessToken) : IQuery<string?>;
}
