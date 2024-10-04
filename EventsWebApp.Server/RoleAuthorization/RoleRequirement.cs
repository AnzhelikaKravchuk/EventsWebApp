using Microsoft.AspNetCore.Authorization;

namespace EventsWebApp.Server.RoleAuthorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role) => Role = role;

        public string Role { get; }
    }
}