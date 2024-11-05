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
    public class GetSocialEventByIdTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public GetSocialEventByIdTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public async void GetSocialEventByIdTests_GetSocialEventByIdHandler_ReturnsSocialEvent()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent();
            SocialEventDto socialEventDto = new SocialEventDto();
            GetSocialEventByIdQuery request = new GetSocialEventByIdQuery(id);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, _cancellationToken)).Returns(socialEvent);
            A.CallTo(() => _mapper.Map<SocialEventDto>(socialEvent)).Returns(socialEventDto);
            GetSocialEventByIdHandler handler = new GetSocialEventByIdHandler(_unitOfWork, _mapper);

            //Act
            SocialEventDto entity = await handler.Handle(request, _cancellationToken);

            //Assert
            entity.Should().NotBeNull();
            entity.Should().BeOfType<SocialEventDto>();
        }

        [Fact]
        public async void GetSocialEventByIdTests_GetSocialEventByIdHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid id = Guid.NewGuid();
            GetSocialEventByIdQuery request = new GetSocialEventByIdQuery(id);
            GetSocialEventByIdHandler handler = new GetSocialEventByIdHandler(_unitOfWork, _mapper);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void GetSocialEventByIdTests_GetSocialEventByIdHandler_ThrowsException()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            SocialEvent? socialEvent = null;
            GetSocialEventByIdQuery request = new GetSocialEventByIdQuery(id);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, _cancellationToken)).Returns(socialEvent);
            GetSocialEventByIdHandler handler = new GetSocialEventByIdHandler(_unitOfWork, _mapper);

            //Act
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(()=>  handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }

    }
}
