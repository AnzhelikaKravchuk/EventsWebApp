using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserCofiguration().Configure(builder.Entity<User>());
        }
    }
}
