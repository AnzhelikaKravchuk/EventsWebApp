using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.UseCases.SocialEvents.Queries;
using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using FakeItEasy;
using FluentAssertions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.QueriesTests
{
    public class GetPaginatedSocialEventsTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public GetPaginatedSocialEventsTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void GetPaginatedSocialEventsTests_GetPaginatedSocialEventsHandler_ReturnsPaginatedListSocialEvent()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            int pageIndex = 10;
            int pageSize = 10;
            int totalPages = 10;
            List<SocialEvent> socialEvents = new List<SocialEvent>();
            SocialEventDto socialEventDto = A.Fake<SocialEventDto>();
            AppliedFilters appliedFilters = A.Fake<AppliedFilters>();
            GetPaginatedSocialEventsQuery request = new GetPaginatedSocialEventsQuery(appliedFilters, pageIndex, pageSize);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetSocialEvents(appliedFilters, pageIndex, pageSize, _cancellationToken)).Returns((socialEvents,totalPages));
            A.CallTo(() => _mapper.Map<SocialEventDto>(A<SocialEvent>.Ignored)).Returns(socialEventDto);
            GetPaginatedSocialEventsHandler handler = new GetPaginatedSocialEventsHandler(_unitOfWork, _mapper);

            //Act
            PaginatedList<SocialEventDto> list = await handler.Handle(request, _cancellationToken);

            //Assert
            list.Should().NotBeNull();
            list.Should().BeOfType<PaginatedList<SocialEventDto>>();
        }

        [Fact]
        public async void GetPaginatedSocialEventsTests_GetPaginatedSocialEventsHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            int pageIndex = 10;
            int pageSize = 10;
            
            AppliedFilters appliedFilters = A.Fake<AppliedFilters>();
            GetPaginatedSocialEventsQuery request = new GetPaginatedSocialEventsQuery(appliedFilters, pageIndex, pageSize);
            GetPaginatedSocialEventsHandler handler = new GetPaginatedSocialEventsHandler(_unitOfWork, _mapper);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(()=>handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }
    }
}
