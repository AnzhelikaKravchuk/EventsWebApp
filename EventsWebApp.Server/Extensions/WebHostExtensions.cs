using EventsWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Server.Extensions
{
    public static class WebHostExtensions
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return webApp;
        }
    }
}