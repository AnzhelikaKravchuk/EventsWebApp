using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.UseCases.SocialEvents.Queries;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.QueriesTests
{
    public class GetSocialEventByNameTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public GetSocialEventByNameTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public async void GetSocialEventByNameTests_GetSocialEventByNameHandler_ReturnsSocialEvent()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            string name = "Book";
            GetSocialEventByNameQuery request = new GetSocialEventByNameQuery(name);
            SocialEvent socialEvent = new SocialEvent { EventName = "Book" };
            SocialEventDto socialEventDto = new SocialEventDto();
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByName(request.Name, _cancellationToken)).Returns(socialEvent);
            A.CallTo(() => _mapper.Map<SocialEventDto>(socialEvent)).Returns(socialEventDto);
            GetSocialEventByNameHandler handler = new GetSocialEventByNameHandler(_unitOfWork,_mapper);

            //Act
            SocialEventDto entity = await handler.Handle(request,_cancellationToken);

            //Assert
            entity.Should().NotBeNull();
            entity.Should().BeOfType<SocialEventDto>();
        }

        [Fact]
        public async void GetSocialEventByNameTests_GetSocialEventByNameHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            string name = "Book";
            GetSocialEventByNameQuery request = new GetSocialEventByNameQuery(name);
            GetSocialEventByNameHandler handler = new GetSocialEventByNameHandler(_unitOfWork, _mapper);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void GetSocialEventByNameTests_GetSocialEventByNameHandler_ThrowsException()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            string name = "Book";
            GetSocialEventByNameQuery request = new GetSocialEventByNameQuery(name);
            SocialEvent? socialEvent = null;
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByName(request.Name, _cancellationToken)).Returns(socialEvent);
            GetSocialEventByNameHandler handler = new GetSocialEventByNameHandler(_unitOfWork, _mapper);

            //Act
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request,_cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }
    }
}
