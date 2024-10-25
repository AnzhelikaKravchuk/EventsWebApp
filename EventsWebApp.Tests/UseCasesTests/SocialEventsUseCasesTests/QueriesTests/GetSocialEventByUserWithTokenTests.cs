using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.UseCases.SocialEvents.Queries;
using FakeItEasy;
using FluentAssertions;
using System.Security.Claims;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Exceptions;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.QueriesTests
{
    public class GetSocialEventByUserWithTokenTests
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public GetSocialEventByUserWithTokenTests()
        {
            _unitOfWork = A.Fake<IAppUnitOfWork>();
            _mapper = A.Fake<IMapper>();
            _jwtProvider = A.Fake<IJwtProvider>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void GetSocialEventByUserWithTokenTests_GetSocialEventByUserWithTokenHandler_ReturnsSocialEventTrue(bool hasAttendee)
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            GetSocialEventByUserWithTokenQuery request = new GetSocialEventByUserWithTokenQuery(id,accessToken);
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            SocialEventDto socialEventDto = new SocialEventDto { ListOfAttendees = new List<Attendee> { hasAttendee? new Attendee { UserId = userId}: null } };
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByIdWithInclude(id, _cancellationToken)).Returns(socialEvent);
            A.CallTo(()=>_mapper.Map<SocialEventDto>(socialEvent)).Returns(socialEventDto);
            GetSocialEventByUserWithTokenHandler handler = new GetSocialEventByUserWithTokenHandler(_unitOfWork, _mapper,_jwtProvider);

            //Act
            SocialEventDto entity = await handler.Handle(request, _cancellationToken);

            //Assert
            entity.Should().NotBeNull();
            entity.Should().BeOfType<SocialEventDto>();
            entity.IsAlreadyInList.Should().Be(hasAttendee);
        }

        [Fact]
        public async void GetSocialEventByUserWithTokenTests_GetSocialEventByUserWithTokenHandler_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            GetSocialEventByUserWithTokenQuery request = new GetSocialEventByUserWithTokenQuery(id, accessToken);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            GetSocialEventByUserWithTokenHandler handler = new GetSocialEventByUserWithTokenHandler(_unitOfWork, _mapper, _jwtProvider);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void GetSocialEventByUserWithTokenTests_GetSocialEventByUserWithTokenHandler_ThrowsExceptionToken()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            string accessToken = string.Empty;
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            GetSocialEventByUserWithTokenQuery request = new GetSocialEventByUserWithTokenQuery(id, accessToken);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            GetSocialEventByUserWithTokenHandler handler = new GetSocialEventByUserWithTokenHandler(_unitOfWork, _mapper, _jwtProvider);

            //Act
            TokenException exception = await Assert.ThrowsAsync<TokenException>(() => handler.Handle(request,_cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("Invalid token");
        }

        [Fact]
        public async void SocialEventsServiceTests_GetSocialEventByIdWithToken_ThrowsException()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string accessToken = string.Empty;
            GetSocialEventByUserWithTokenQuery request = new GetSocialEventByUserWithTokenQuery(id, accessToken);
            SocialEvent? socialEvent = null;
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", userId.ToString()) }));
            A.CallTo(() => _jwtProvider.GetPrincipalFromExpiredToken(accessToken)).Returns(claimsPrincipal);
            A.CallTo(() => _unitOfWork.SocialEventRepository.GetByIdWithInclude(id,_cancellationToken)).Returns(socialEvent);
            GetSocialEventByUserWithTokenHandler handler = new GetSocialEventByUserWithTokenHandler(_unitOfWork, _mapper, _jwtProvider);

            //Act
            SocialEventException exception = await Assert.ThrowsAsync<SocialEventException>(() => handler.Handle(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No social event was found");
        }
    }
}
