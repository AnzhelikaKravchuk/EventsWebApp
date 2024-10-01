using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

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

            //builder.Entity<SocialEvent>().HasData(
            //    new SocialEvent { Id=new Guid("1"),Name = "First", Description = "Content here", Category = E_SocialEventCategory.Convention, Date = DateTime.Parse("2024-10-25"), MaxAttendee = 15, Place = "Minsk" },
            //    new SocialEvent { Id = new Guid("2"), Name = "Second", Description = "Content here", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2024-10-27"), MaxAttendee = 20, Place = "Gomel" },
            //    new SocialEvent { Id = new Guid("3"), Name = "Third", Description = "Content here", Category = E_SocialEventCategory.Other, Date = DateTime.Parse("2024-11-27"), MaxAttendee = 20, Place = "Gomel" },
            //    new SocialEvent { Id = new Guid("4"), Name = "Forth", Description = "Content here", Category = E_SocialEventCategory.QnA, Date = DateTime.Parse("2024-12-27"), MaxAttendee = 50, Place = "Polotsk" },
            //    new SocialEvent { Id = new Guid("5"), Name = "Fifth", Description = "Content here", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-01-24"), MaxAttendee = 20, Place = "Mogilev" },
            //    new SocialEvent { Id = new Guid("6"), Name = "Sixth", Description = "Content here", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-10-11"), MaxAttendee = 55, Place = "Vitebsk" },
            //    new SocialEvent { Id = new Guid("7"), Name = "Seventh", Description = "Content here", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-10-27"), MaxAttendee = 100, Place = "Brest" },
            //    new SocialEvent { Id = new Guid("8"), Name = "Eigth", Description = "Content here", Category = E_SocialEventCategory.QnA, Date = DateTime.Parse("2025-11-16"), MaxAttendee = 10, Place = "Grodno" },
            //    new SocialEvent { Id = new Guid("9"), Name = "Nineth", Description = "Content here", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-04-01"), MaxAttendee = 30, Place = "Minsk" },
            //    new SocialEvent { Id = new Guid("10"), Name = "Tenth", Description = "Content here", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2025-10-02"), MaxAttendee = 50, Place = "Minsk" }
            //    );

            //builder.Entity<Attendee>().HasData(
            //    new Attendee(new Guid("1"), Name = "First", Description = "Content here", Category = E_SocialEventCategory.Convention, Date = DateTime.Parse("2024-10-25"), MaxAttendee = 15, Place = "Minsk" },
            //    new SocialEvent { Name = "Second", Description = "Content here", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2024-10-27"), MaxAttendee = 20, Place = "Gomel" },
            //    new SocialEvent { Name = "Third", Description = "Content here", Category = E_SocialEventCategory.Other, Date = DateTime.Parse("2024-11-27"), MaxAttendee = 20, Place = "Gomel" },
            //    new SocialEvent { Name = "Forth", Description = "Content here", Category = E_SocialEventCategory.QnA, Date = DateTime.Parse("2024-12-27"), MaxAttendee = 50, Place = "Polotsk" },
            //    new SocialEvent { Name = "Fifth", Description = "Content here", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-01-24"), MaxAttendee = 20, Place = "Mogilev" },
            //    new SocialEvent { Name = "Sixth", Description = "Content here", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-10-11"), MaxAttendee = 55, Place = "Vitebsk" },
            //    new SocialEvent { Name = "Seventh", Description = "Content here", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-10-27"), MaxAttendee = 100, Place = "Brest" },
            //    new SocialEvent { Name = "Eigth", Description = "Content here", Category = E_SocialEventCategory.QnA, Date = DateTime.Parse("2025-11-16"), MaxAttendee = 10, Place = "Grodno" },
            //    new SocialEvent { Name = "Nineth", Description = "Content here", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-04-01"), MaxAttendee = 30, Place = "Minsk" },
            //    new SocialEvent { Name = "Tenth", Description = "Content here", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2025-10-02"), MaxAttendee = 50, Place = "Minsk" }
            //    );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ESUGBMO;Database=EventsWebApp;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
