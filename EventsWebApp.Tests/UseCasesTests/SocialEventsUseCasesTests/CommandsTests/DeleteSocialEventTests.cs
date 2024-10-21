using AutoMapper;
using EventsWebApp.Application.SocialEvents.Commands;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.CommandsTests
{
    public class DeleteSocialEventTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public DeleteSocialEventTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
        }

        [Fact]
        public async void DeleteSocialEventTests_DeleteSocialEventHandler_ReturnsId()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            DeleteSocialEventCommand socialEventRequest = new DeleteSocialEventCommand(id);
            int rowsDeleted = 1;
            A.CallTo(() => _unitOfWork.SocialEventRepository.Delete(socialEventRequest.Id, _cancellationToken)).Returns(rowsDeleted);
            DeleteSocialEventHandler handler = new DeleteSocialEventHandler(_unitOfWork);

            //Act
            Guid result = await handler.Handle(socialEventRequest, _cancellationToken);

            //Assert
            result.Should().NotBeEmpty();
            result.Should().Be(id);
        }

        [Fact]
        public async void DeleteSocialEventTests_DeleteSocialEventHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid id = Guid.NewGuid();
            DeleteSocialEventCommand socialEventRequest = new DeleteSocialEventCommand(id);
            int rowsDeleted = 1;
            A.CallTo(() => _unitOfWork.SocialEventRepository.Delete(socialEventRequest.Id, _cancellationToken)).Returns(rowsDeleted);
            DeleteSocialEventHandler handler = new DeleteSocialEventHandler(_unitOfWork);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(socialEventRequest, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void DeleteSocialEventTests_DeleteSocialEventHandler_ThrowsNoRowsDeleted()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            DeleteSocialEventCommand socialEventRequest = new DeleteSocialEventCommand(id);
            int rowsDeleted = 0;
            A.CallTo(() => _unitOfWork.SocialEventRepository.Delete(socialEventRequest.Id, _cancellationToken)).Returns(rowsDeleted);
            DeleteSocialEventHandler handler = new DeleteSocialEventHandler(_unitOfWork);

            //Act
            SocialEventException exception = await Assert.ThrowsAsync<SocialEventException>(() => handler.Handle(socialEventRequest, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Social event wasn't deleted");
        }
    }
}
