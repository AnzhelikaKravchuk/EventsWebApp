using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsWebApp.Infrastructure.Configurations
{
    public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.HasOne(x => x.SocialEvent).WithMany(e => e.ListOfAttendees);
            builder.HasOne(x => x.User).WithMany();
        }
    }
}
