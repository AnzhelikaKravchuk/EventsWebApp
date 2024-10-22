using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.UseCases.SocialEvents.Queries;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.QueriesTests
{
    public class GetAttendeesByEventIdTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public GetAttendeesByEventIdTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void GetAttendeesByEventIdTests_GetAttendeesByEventIdHandler_ReturnsAttendeesList()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
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
            AttendeeDto attendeeResponse = A.Fake<AttendeeDto>();
            GetAttendeesByEventIdQuery request = new GetAttendeesByEventIdQuery(id); 
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(request.Id, _cancellationToken)).Returns(socialEvent);
            A.CallTo(() => _mapper.Map<AttendeeDto>(A<Attendee>.Ignored)).Returns(attendeeResponse);
            GetAttendeesByEventIdHandler handler = new GetAttendeesByEventIdHandler(_unitOfWork, _mapper);

            //Act
            List<AttendeeDto> list = await handler.Handle(request, _cancellationToken);

            //Assert
            list.Should().NotBeNull();
            list.Should().BeOfType<List<AttendeeDto>>();
        }

        [Fact]
        public async void GetAttendeesByEventIdTests_GetAttendeesByEventIdHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid id = Guid.NewGuid();
            GetAttendeesByEventIdQuery request = new GetAttendeesByEventIdQuery(id);
            GetAttendeesByEventIdHandler handler = new GetAttendeesByEventIdHandler(_unitOfWork, _mapper);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }


        [Fact]
        public async void GetAttendeesByEventIdTests_GetAttendeesByEventIdHandler_ThrowsException()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            SocialEvent? socialEvent = null;
            GetAttendeesByEventIdQuery request = new GetAttendeesByEventIdQuery(id); 
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetById(request.Id, _cancellationToken)).Returns(socialEvent);
            GetAttendeesByEventIdHandler handler = new GetAttendeesByEventIdHandler(_unitOfWork, _mapper);

            //Act
            SocialEventException exception = await Assert.ThrowsAsync<SocialEventException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }
    }
}
