using AutoMapper;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.SocialEvents.Commands;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.CommandsTests
{
    public class UpdateSocialEventTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public UpdateSocialEventTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
            _emailSender = A.Fake<IEmailSender>();
        }

        [Fact]
        public async void UpdateSocialEventTests_UpdateSocialEventHandler_ReturnsId()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
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
            UpdateSocialEventCommand request = new UpdateSocialEventCommand
            {
                Id = socialEvent.Id,
                EventName = "Climate Change Conference",
                Description = socialEvent.Description,
                Date = "2026-11-11 00:00:00.0000000",
                Category = E_SocialEventCategory.Conference.ToString(),
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png"
            };
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(request.Id,_cancellationToken)).Returns(socialEvent);
            A.CallTo(() => _mapper.Map<SocialEvent>(request)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.Update(socialEvent, _cancellationToken)).Returns(updatedEvent.Id);
            UpdateSocialEventHandler handler = new UpdateSocialEventHandler(_unitOfWork,_mapper, _emailSender);

            //Act
            Guid id = await handler.Handle(request, _cancellationToken);

            //Assert
            id.Should().NotBeEmpty();
            id.Should().Be(socialEvent.Id);
        }

        [Fact]
        public async void UpdateSocialEventTests_UpdateSocialEventHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            UpdateSocialEventCommand request = new UpdateSocialEventCommand
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Content",
                Date = "2026-11-11 00:00:00.0000000",
                Category = E_SocialEventCategory.Conference.ToString(),
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png"
            };
            UpdateSocialEventHandler handler = new UpdateSocialEventHandler(_unitOfWork, _mapper, _emailSender);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void UpdateSocialEventTests_UpdateSocialEventHandler_ThrowsException()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            SocialEvent? socialEvent = null;
            UpdateSocialEventCommand request = new UpdateSocialEventCommand
            {
                Id = Guid.NewGuid(),
                EventName = "Climate Change Conference",
                Description = "Content",
                Date = "2026-11-11 00:00:00.0000000",
                Category = E_SocialEventCategory.Conference.ToString(),
                Place = "Minsk",
                MaxAttendee = 20,
                Image = "image.png"
            };
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(request.Id, _cancellationToken)).Returns(socialEvent);
            UpdateSocialEventHandler handler = new UpdateSocialEventHandler(_unitOfWork, _mapper, _emailSender);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event found");
        }
        [Fact]
        public async void UpdateSocialEventTests_UpdateSocialEventHandler_ThrowsExceptionMaxAttendee()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
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
                ListOfAttendees = new List<Attendee> { new Attendee(), new Attendee() }
            };
            UpdateSocialEventCommand request = new UpdateSocialEventCommand
            {
                Id = socialEvent.Id,
                EventName = "Climate Change Conference",
                Description = socialEvent.Description,
                Date = "2026-11-11 00:00:00.0000000",
                Category = E_SocialEventCategory.Conference.ToString(),
                Place = "Minsk",
                MaxAttendee = 1,
                Image = "image.png"
            };
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(request.Id, _cancellationToken)).Returns(socialEvent);
            UpdateSocialEventHandler handler = new UpdateSocialEventHandler(_unitOfWork, _mapper, _emailSender);

            //Act
            var exception = await Assert.ThrowsAsync<SocialEventException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Can't lower max attendee number");
        }
    }
}
