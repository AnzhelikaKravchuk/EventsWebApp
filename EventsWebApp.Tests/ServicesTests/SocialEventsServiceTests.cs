using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.Repositories;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Application.Services;
using EventsWebApp.Application.Validators;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using FakeItEasy;
using FluentAssertions;
using System.Reflection;
using System.Security.Claims;

namespace EventsWebApp.Tests.ServicesTests
{
    public class SocialEventsServiceTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IAttendeeService _attendeeService;
        private readonly IEmailSender _emailSender;
        private readonly SocialEventValidator _validator;

        public SocialEventsServiceTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _jwtProvider = A.Fake<IJwtProvider>();
            _attendeeService = A.Fake<IAttendeeService>();
            _emailSender = A.Fake<IEmailSender>();
            _validator = new SocialEventValidator();
        }
        //-------------------------------GetAllSocialEvents-----------------------

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEvents_ReturnsPaginatedListSocialEvent()
        {
            //Arrange
            int pageIndex = 10;
            int pageSize = 10;
            var socialEvents = new PaginatedList<SocialEvent>();
            var appliedFilters = A.Fake<AppliedFilters>();
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetSocialEvents(appliedFilters, pageIndex, pageSize)).Returns(socialEvents);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var list = await socialEventService.GetSocialEvents(appliedFilters, pageIndex, pageSize);

            //Assert
            list.Should().NotBeNull();
            list.Should().BeOfType<PaginatedList<SocialEvent>>();
        }

        //-------------------------------GetSocialEventById-----------------------
        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventById_ReturnsSocialEvent()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var socialEvent = new SocialEvent();
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var entity = await socialEventService.GetSocialEventById(id);

            //Assert
            entity.Should().NotBeNull();
            entity.Should().BeOfType<SocialEvent>();
        }

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventById_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            SocialEvent socialEvent = null;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.GetSocialEventById(id));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }

        //-------------------------------GetSocialEventByIdWithToken-----------------------
        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventByIdWithToken_ReturnsSocialEventAndFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            var socialEvent = new SocialEvent { ListOfAttendees = new List<Attendee> { } }; 
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var entity = await socialEventService.GetSocialEventByIdWithToken(id, accessToken);

            //Assert
            entity.Should().NotBeNull();
            entity.Item1.Should().BeOfType<SocialEvent>();
            entity.Item2.Should().Be(false);
        }

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventByIdWithToken_ReturnsSocialEventAndTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            var socialEvent = new SocialEvent { ListOfAttendees = new List<Attendee> { new Attendee { UserId = userId } } };
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var entity = await socialEventService.GetSocialEventByIdWithToken(id, accessToken);

            //Assert
            entity.Should().NotBeNull();
            entity.Item1.Should().BeOfType<SocialEvent>();
            entity.Item2.Should().Be(true);
        }

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventByIdWithToken_ThrowsExceptionToken()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string accessToken = string.Empty;
            var socialEvent = new SocialEvent();
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var exception = await Assert.ThrowsAsync<TokenException>(()=>socialEventService.GetSocialEventByIdWithToken(id, accessToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Invalid token");
        }

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventByIdWithToken_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            SocialEvent socialEvent = null;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.GetSocialEventByIdWithToken(id, accessToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }

        //-------------------------------GetAttendeesById-----------------------
        [Fact]
        public async void SocialEventsServiceTests_GetAttendeesById_ReturnsAttendeesList()
        {
            //Arrange
            Guid id = Guid.NewGuid();
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
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var list = await socialEventService.GetAttendeesById(id);

            //Assert
            list.Should().NotBeNull();
            list.Should().BeOfType<List<Attendee>>();
        }

        public async void SocialEventsServiceTests_GetAttendeesById_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            SocialEvent socialEvent = null;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.GetAttendeesById(id));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }

        //-------------------------------CreateSocialEvent-----------------------
        [Fact]
        public async void SocialEventsServiceTests_CreateSocialEvent_ReturnsId()
        {
            //Arrange
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

            A.CallTo(() => _unitOfWork.SocialEventRepository.Add(socialEvent)).Returns(socialEvent.Id);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var id = await socialEventService.CreateSocialEvent(socialEvent);

            //Assert
            id.Should().NotBeEmpty();
            id.Should().Be(socialEvent.Id);
        }

        //-------------------------------UpdateSocialEvent-----------------------
        [Fact]
        public async void SocialEventsServiceTests_UpdateSocialEvent_ReturnsId()
        {
            //Arrange
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
            SocialEvent updatedEvent = new SocialEvent
            {
                Id = socialEvent.Id,
                EventName = "Climate Change Conference",
                Description = socialEvent.Description,
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(updatedEvent.Id)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.Update(updatedEvent)).Returns(updatedEvent.Id);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var id = await socialEventService.UpdateSocialEvent(updatedEvent);

            //Assert
            id.Should().NotBeEmpty();
            id.Should().Be(socialEvent.Id);
        }

        [Fact]
        public async void SocialEventsServiceTests_UpdateSocialEvent_ThrowsException()
        {
            //Arrange
            SocialEvent socialEvent = null;
            SocialEvent updatedEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(updatedEvent.Id)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.UpdateSocialEvent(updatedEvent));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event found");
        }


        //-------------------------------DeleteSocialEvent-----------------------
        [Fact]
        public async void SocialEventsServiceTests_DeleteSocialEvent_ReturnsId()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            int rowsDeleted = 1;
            A.CallTo(() => _unitOfWork.SocialEventRepository.Delete(id)).Returns(rowsDeleted);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var result = await socialEventService.DeleteSocialEvent(id);

            //Assert
            result.Should().NotBeEmpty();
            result.Should().Be(id);
        }

        [Fact]
        public async void SocialEventsServiceTests_DeleteSocialEvent_ThrowsException()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            int rowsDeleted = 0;
            A.CallTo(() => _unitOfWork.SocialEventRepository.Delete(id)).Returns(rowsDeleted);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var result = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.DeleteSocialEvent(id));

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Social event wasn't deleted");
        }


        //-------------------------------AddAttendeeToEvent-----------------------
        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEvent_ReturnsId()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid attendeeId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            attendee.Id = attendeeId;
            Attendee candidate = null;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(socialEvent.Id)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEvent.Id, attendee.Email)).Returns(candidate);
            A.CallTo(() => _attendeeService.AddAttendee(attendee, socialEvent, userId)).Returns(attendee);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);
            
            //Act
            var result = await socialEventService.AddAttendeeToEvent(socialEvent.Id, attendee, userId);
            
            //Assert
            result.Should().NotBeEmpty();
            result.Should().Be(attendeeId);
        }

        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEvent_ThrowsExceptionSocialEvent()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid socialEventId = Guid.NewGuid();
            SocialEvent socialEvent = null;
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(socialEventId)).Returns(socialEvent);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var result = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.AddAttendeeToEvent(socialEventId, attendee, userId));

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("No social event was found");
        }
        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEvent_ThrowsExceptionAttendeeExists()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            socialEvent.ListOfAttendees.Add(attendee);
            attendee.SocialEvent = socialEvent;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(socialEvent.Id)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEvent.Id, attendee.Email)).Returns(attendee);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            var result = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.AddAttendeeToEvent(socialEvent.Id, attendee, userId));

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("This attendee already in the list");
        }

        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEvent_ThrowsExceptionMaxAttendeesReached()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 1,
                Image = "image.png",
                ListOfAttendees = new List<Attendee> { new Attendee() }
            };
            Attendee candidate = null;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(socialEvent.Id)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEvent.Id, attendee.Email)).Returns(candidate);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);
            
            //Act
            var result = await Assert.ThrowsAsync<SocialEventException>(() => socialEventService.AddAttendeeToEvent(socialEvent.Id, attendee, userId));

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Max attendee number reached");
        }


        //-------------------------------AddAttendeeToEventWithToken-----------------------
        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEventWithToken_ReturnsId()
        {
            //Arrange
            string accessToken = string.Empty;
            Guid userId = Guid.NewGuid();
            Guid attendeeId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            attendee.Id = attendeeId;
            Attendee candidate = null;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(socialEvent.Id)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetAttendeeByEmail(socialEvent.Id, attendee.Email)).Returns(candidate);
            A.CallTo(() => _attendeeService.AddAttendee(attendee, socialEvent, userId)).Returns(attendee);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);
            
            //Act
            var result = await socialEventService.AddAttendeeToEventWithToken(socialEvent.Id, attendee, accessToken);
            
            //Assert
            result.Should().NotBeEmpty();
            result.Should().Be(attendeeId);
        }

        [Fact]
        public async void SocialEventsServiceTests_AddAttendeeToEventWithToken_ThrowsException()
        {
            //Arrange
            string accessToken = string.Empty;
            Guid userId = Guid.NewGuid();
            Guid attendeeId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            Attendee attendee = new Attendee("Test", "Testoviy", "ex@gmail.com", DateTime.Now.AddDays(-1), DateTime.Now, null, null);
            attendee.Id = attendeeId;
            Attendee candidate = null;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);
            
            //Act
            var result = await Assert.ThrowsAsync<TokenException>(() => socialEventService.AddAttendeeToEventWithToken(socialEvent.Id, attendee, accessToken));

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Invalid token");
        }

        //-------------------------------ValidateSocialEvent-----------------------
        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_Exists()
        {
            //Arrange
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);
            
            //Act
            MethodInfo method = typeof(SocialEventService).GetMethod("ValidateSocialEvent", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);
            //Assert
            method.Should().NotBeNull();
        }

        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_IsValid()
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Parse("2026-11-11 00:00:00.0000000"),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            MethodInfo method = typeof(SocialEventService).GetMethod("ValidateSocialEvent", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);
            //Act
            method.Invoke(socialEventService, [socialEvent]);

            //Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-1000)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidDate(int days)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Description",
                Date = DateTime.Now.AddDays(days),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Date' must be greater");
        }

        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidEventName()
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = string.Empty,
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Event Name' must not be empty");
        }

        [Theory]
        [InlineData(101)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidLength(int length)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = new string('1', length),
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "The length of 'Event Name' must be 100 characters or fewer.");
        }

        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidDescription()
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = string.Empty,
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Description' must not be empty");
        }

        [Theory]
        [InlineData(1001)]
        [InlineData(2001)]
        [InlineData(100_000)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionMaxDescription(int length)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = new string('1', length),
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.Conference,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "The length of 'Description' must be 1000 characters or fewer.");
        }

        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidCategory()
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.None,
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Category' must not be empty");
        }

        [Fact]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionInvalidPlace()
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.None,
                Place = string.Empty,
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Place' must not be empty");
        }

        [Theory]
        [InlineData(101)]
        [InlineData(200)]
        [InlineData(10000)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionMaxLength(int length)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.None,
                Place = new string('1', length),
                MaxAttendee = 20,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };
            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "The length of 'Place' must be 100 characters or fewer.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-10000)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionGreaterThanZero(int maxAttendee)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.None,
                Place = "Place",
                MaxAttendee = maxAttendee,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };

            //Act - Assert
            ValidateSocialEvent_HelperFunction(socialEvent, "'Max Attendee' must be greater");
        }

        [Theory]
        [InlineData(100_001)]
        [InlineData(200_001)]
        [InlineData(1000001)]
        public void SocialEventsServiceTests_ValidateSocialEvent_ThrowsExceptionLowerThan(int maxAttendee)
        {
            //Arrange
            SocialEvent socialEvent = new SocialEvent
            {
                Id = Guid.NewGuid(),
                EventName = "Book meeting",
                Description = "Description",
                Date = DateTime.Now.AddDays(1),
                Category = E_SocialEventCategory.None,
                Place = "Place",
                MaxAttendee = maxAttendee,
                Image = "image.png",
                ListOfAttendees = new List<Attendee>()
            };

            ValidateSocialEvent_HelperFunction(socialEvent, "'Max Attendee' must be less");
        }

        private void ValidateSocialEvent_HelperFunction(SocialEvent socialEvent, string message)
        {
            //Arrange
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            MethodInfo method = typeof(SocialEventService).GetMethod("ValidateSocialEvent", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);

            //Act
            var exception = Assert.Throws<TargetInvocationException>(() => method.Invoke(socialEventService, [socialEvent]));

            //Assert
            exception.Should().NotBeNull();
            exception.InnerException.Message.Should().Contain(message);
        }


        //-------------------------------CheckToken-----------------------
        [Fact]
        public void SocialEventsServiceTests_CheckToken_Exists()
        {
            //Arrange
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            //Act
            MethodInfo method = typeof(SocialEventService).GetMethod("CheckToken", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);
            //Assert
            method.Should().NotBeNull();
        }

        [Fact]
        public void SocialEventsServiceTests_CheckToken_IsValid()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            MethodInfo method = typeof(SocialEventService).GetMethod("CheckToken", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);
            //Act
            var result = method.Invoke(socialEventService, [accessToken]);

            //Assert
            result.Should().NotBeNull();
            result.Should().Be(userId);
        }

        [Fact]
        public void SocialEventsServiceTests_CheckToken_ThrowsException()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            var socialEventService = new SocialEventService(_unitOfWork, _jwtProvider, _attendeeService, _emailSender, _validator);

            MethodInfo method = typeof(SocialEventService).GetMethod("CheckToken", BindingFlags.NonPublic
                                                                                            | BindingFlags.Instance);
            //Act
            var exception = Assert.Throws<TargetInvocationException>(()=>method.Invoke(socialEventService, [accessToken]));

            //Assert
            exception.Should().NotBeNull();
            exception.InnerException.Message.Should().Be("Invalid token");
        }
    }
}
