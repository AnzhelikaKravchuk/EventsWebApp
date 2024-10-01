using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Infrastructure.Configurations
{
    public class SocialEventConfiguration : IEntityTypeConfiguration<SocialEvent>
    {
        public void Configure(EntityTypeBuilder<SocialEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Place).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.MaxAttendee).IsRequired();
            builder.Property(x => x.Image);
            builder.HasMany(x => x.ListOfAttendees).WithOne(e => e.SocialEvent);
        }
    }
}
