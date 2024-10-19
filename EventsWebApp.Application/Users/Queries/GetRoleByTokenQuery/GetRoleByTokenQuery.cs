using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.Users.Queries.GetRoleByTokenQuery
{
    public record GetRoleByTokenQuery(string AccessToken) : IQuery<string?>;
}
