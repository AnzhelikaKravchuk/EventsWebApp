using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SocialEvent> SocialEvents { get; set; } = null!;
        public DbSet<Attendee> Attendees { get; set; } = null!;
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            new UserConfiguration().Configure(builder.Entity<User>());
            new SocialEventConfiguration().Configure(builder.Entity<SocialEvent>());
            new AttendeeConfiguration().Configure(builder.Entity<Attendee>());
        }
    }
}
