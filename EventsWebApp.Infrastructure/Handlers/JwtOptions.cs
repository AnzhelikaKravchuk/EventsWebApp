namespace EventsWebApp.Infrastructure.Handlers
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiresTimeAccess { get; set; }
        public int ExpiresTimeRefresh { get; set; }
    }
}
