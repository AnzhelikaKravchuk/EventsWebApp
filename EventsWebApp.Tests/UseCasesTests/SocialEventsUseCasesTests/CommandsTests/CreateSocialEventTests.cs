using AutoMapper;
using EventsWebApp.Application.SocialEvents.Commands;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.CommandsTests
{
    public class CreateSocialEventTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public CreateSocialEventTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void CreateSocialEventTests_CreateSocialEventHandler_ReturnsId()
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

            CreateSocialEventCommand socialEventRequest = A.Fake<CreateSocialEventCommand>();
            A.CallTo(() => _mapper.Map<SocialEvent>(socialEventRequest)).Returns(socialEvent);
            A.CallTo(() => _unitOfWork.SocialEventRepository.Add(socialEvent, _cancellationToken)).Returns(socialEvent.Id);
            CreateSocialEventHandler handler = new CreateSocialEventHandler(_unitOfWork, _mapper);

            //Act
            Guid id = await handler.Handle(socialEventRequest, _cancellationToken);

            //Assert
            id.Should().NotBeEmpty();
            id.Should().Be(socialEvent.Id);
        }

        [Fact]
        public async void CreateSocialEventTests_CreateSocialEventHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            CreateSocialEventCommand socialEventRequest = A.Fake<CreateSocialEventCommand>();
            CreateSocialEventHandler handler = new CreateSocialEventHandler(_unitOfWork, _mapper);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(socialEventRequest, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }
    }
}
