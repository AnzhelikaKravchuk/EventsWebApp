using EventsWebApp.Infrastructure.Configurations;
using EventsWebApp.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserCofiguration().Configure(builder.Entity<UserEntity>());
        }
    }
}
