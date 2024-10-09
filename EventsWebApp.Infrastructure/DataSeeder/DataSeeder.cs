using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Infrastructure.Handlers;

namespace EventsWebApp.Infrastructure.DataSeeder
{
    public class DataSeeder
    {
        private readonly PasswordHasher passwordHasher = new PasswordHasher();
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            List<SocialEvent> events = new List<SocialEvent>{
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Trivia Night Extravaganza", Description = "A fun-filled trivia night where teams compete to answer questions across various categories. Great prizes await the winners!", Category = E_SocialEventCategory.Other, Date = DateTime.Parse("2025-10-27"), MaxAttendee = 1, Place = "Minsk", Image="images\\dc7ea763-8d70-43d5-bf36-c05242b31029-изображение_2024-10-08_024226418.png" },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Marketing Strategies Conference", Description = "A comprehensive conference focused on the latest trends and techniques in digital marketing, featuring expert speakers and interactive sessions.", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2025-10-27"), MaxAttendee = 2, Place = "Gomel", Image="images\\5832d92a-867b-4b6d-9830-9bc7b904b403-изображение_2024-10-09_031543412.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Tech Innovations Convention", Description = "An exciting convention showcasing the latest technology and innovations. Meet industry leaders, attend panel discussions, and explore cutting-edge products.", Category = E_SocialEventCategory.Convention, Date = DateTime.Parse("2025-11-27"), MaxAttendee = 20, Place = "Gomel", Image="images\\9bf4f2d6-2dca-490c-ae87-3cba6134c413-изображение_2024-10-09_031528191.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Financial Freedom Q&A Session", Description = "An interactive Q&A session with financial experts who will provide insights on personal finance management, investment strategies, and wealth building.", Category = E_SocialEventCategory.QnA, Date = DateTime.Parse("2025-12-27"), MaxAttendee = 50, Place = "Polotsk", Image="images\\fcefd765-202c-433b-b98c-b03065d019a5-изображение_2024-10-08_024038630.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Modern Art Lecture", Description = "A lecture by a renowned art historian covering the evolution and impact of modern art movements in the 20th and 21st centuries.", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-01-24"), MaxAttendee = 20, Place = "Mogilev", Image="images\\be1df92e-4250-4ce7-8d3e-b82b1a3ea6fe-изображение_2024-10-08_024330991.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Entrepreneurship MasterClass", Description = "A masterclass led by a successful entrepreneur, sharing insights on how to start and grow a business, including tips on funding, marketing, and scaling.", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-10-11"), MaxAttendee = 55, Place = "Vitebsk", Image="images\\4b4fd120-0fce-49ef-b9fb-178c5eba4f72-изображение_2024-10-08_024111696.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Climate Change Conference", Description = "A conference discussing the impact of climate change, featuring experts from environmental science, policy-making, and sustainable practices.", Category = E_SocialEventCategory.Conference, Date = DateTime.Parse("2025-10-11"), MaxAttendee = 100, Place = "Brest", Image="images\\d57d7aba-fa51-48f8-bc13-8a250489e219-изображение_2024-10-08_024142528.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Health & Wellness Lecture", Description = "A lecture by a health expert on holistic wellness practices, covering topics like nutrition, mental health, and exercise for a balanced lifestyle.", Category = E_SocialEventCategory.Lecture, Date = DateTime.Parse("2025-10-11"), MaxAttendee = 10, Place = "Grodno", Image="images\\e4343661-d697-49ed-bf9c-cb10a4ec7cf4-изображение_2024-10-08_023959172.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Photography MasterClass", Description = "A hands-on masterclass with a professional photographer, focusing on advanced techniques in portrait and landscape photography.", Category = E_SocialEventCategory.MasterClass, Date = DateTime.Parse("2025-04-01"), MaxAttendee = 30, Place = "Minsk", Image="images\\ac7044ab-c598-42c0-8624-e5a8f157624b-изображение_2024-10-08_024317048.png"  },
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Leadership Development Convention", Description = "A convention designed for professionals looking to enhance their leadership skills, featuring workshops, panel discussions, and keynote speeches by industry leaders.", Category = E_SocialEventCategory.Convention, Date = DateTime.Parse("2025-10-02"), MaxAttendee = 50, Place = "Minsk", Image="images\\2f85a33e-04a7-42c9-9974-67b033c66d6f-изображение_2024-10-08_023935489.png"  } ,
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Fitness Boot Camp", Description = "A high-energy, outdoor fitness session led by a professional trainer. Suitable for all fitness levels.", Category = E_SocialEventCategory.Other, Date = DateTime.Parse("2025-10-02"), MaxAttendee = 11, Place = "Polotsk", Image="images\\2006cffb-cd31-4fc8-ad90-1e20452dc255-изображение_2024-10-08_024022180.png"  } ,
                    new SocialEvent { Id = Guid.NewGuid(), EventName = "Book Club Gathering", Description = "A monthly book club meeting to discuss the chosen book. Enjoy lively discussions, snacks, and a chance to meet fellow book enthusiasts", Category = E_SocialEventCategory.Other, Date = DateTime.Parse("2025-10-02"), MaxAttendee = 10, Place = "Vitebsk", Image="images\\0dcc6bce-65ee-4555-8495-5b9b8f7e8fc0-изображение_2024-10-08_024204332.png"  }
                };

            List<User> users = new List<User> {
                    new User{ Id = Guid.NewGuid(), Email = "example@example.com", PasswordHash = passwordHasher.Generate("cool"), Role = "User", Username="Jake"},
                    new User { Id = Guid.NewGuid(), Email = "admin@example.com", PasswordHash = passwordHasher.Generate("admin"), Role = "Admin", Username = "Mark" },
                    new User { Id = Guid.NewGuid(), Email = "great@example.com", PasswordHash = passwordHasher.Generate("very"), Role = "User", Username = "Victor" },
                };
            List<Attendee> attendees = new List<Attendee>
                {
                    new Attendee{ Id = Guid.NewGuid(), Name = "Peter", Surname="Parker", Email="example@example.com", DateOfBirth=DateTime.Parse("1960-10-10"), DateOfRegistration = DateTime.Now, SocialEventId=events[0].Id, UserId = users[0].Id },
                    new Attendee{ Id = Guid.NewGuid(), Name = "Bruce", Surname="Banner", Email="great@example.com", DateOfBirth=DateTime.Parse("1980-01-10"), DateOfRegistration = DateTime.Now, SocialEventId=events[1].Id, UserId = users[1].Id  },
                    new Attendee{ Id = Guid.NewGuid(), Name = "Clark", Surname="Kent", Email="great@example.com", DateOfBirth=DateTime.Parse("1990-01-10"), DateOfRegistration = DateTime.Now, SocialEventId=events[2].Id, UserId = users[2].Id  }
                };

            if (!_context.SocialEvents.Any())
            {
                _context.SocialEvents.AddRange(events);
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            { 
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

            if (!_context.Attendees.Any())
            {
                _context.Attendees.AddRange(attendees);
                _context.SaveChanges();
            }
        }
    }
}
