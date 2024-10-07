using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure;
using EventsWebApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Tests.RepositoriesTests
{
    public class SocialEventsRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            if (await context.SocialEvents.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Add(new SocialEvent
                    {
                        Id = i == 0 ? Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA") : Guid.NewGuid(),
                        EventName = "Trivia Night Extravaganza" + i,
                        Description = "A fun-filled trivia night where teams compete to answer questions across various categories. Great prizes await the winners!",
                        Date = DateTime.Parse("2025-01-24 00:00:00.0000000"),
                        Category = E_SocialEventCategory.Other,
                        Place = "Minsk",
                        MaxAttendee = 160,
                        Image = "image.png",
                        ListOfAttendees = new List<Attendee>()
                    });

                    await context.SaveChangesAsync();
                }
            }

            return context;
        }


        [Fact]
        public async void SocialEventsRepository_GetById_ReturnsSocialEvent()
        {
            Guid id = Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA");
            var context = await GetDatabaseContext();
            var socialEventsRepository = new SocialEventRepository(context);

            var result = await socialEventsRepository.GetById(id);

            result.Should().NotBeNull();
            result.Should().BeOfType<SocialEvent>();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async void SocialEventsRepository_GetById_ReturnNull()
        {
            Guid id = Guid.Parse("12345689-1234-1234-1234-1234567890DA");
            var context = await GetDatabaseContext();
            var socialEventsRepository = new SocialEventRepository(context);

            var result = await socialEventsRepository.GetById(id);

            result.Should().BeNull();
        }


        [Fact]
        public async void SocialEventsRepository_Add_ReturnsSocialEventId()
        {
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book Lovers Convention",
                Description = "A monthly book club meeting to discuss the chosen book. Enjoy lively discussions, snacks, and a chance to meet fellow book enthusiasts",
                Date = DateTime.Parse("2025-02-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Convention,
                Place = "Polotsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            var context = await GetDatabaseContext();
            var socialEventsRepository = new SocialEventRepository(context);

            var id = await socialEventsRepository.Add(socialEvent);
            await context.SaveChangesAsync();
            var result = await socialEventsRepository.GetById(id);

            result.Should().NotBeNull();
            result.Should().BeOfType<SocialEvent>();
            result.Id.Should().Be(id);
        }
    }
}