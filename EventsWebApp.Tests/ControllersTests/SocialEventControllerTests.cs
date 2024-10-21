using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Controllers;
using FluentAssertions;
using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.PaginationHandlers;
using EventsWebApp.Application.Dto;
using Microsoft.AspNetCore.Http;
using MediatR;
using EventsWebApp.Application.SocialEvents.Queries;
using EventsWebApp.Application.SocialEvents.Commands;
using EventsWebApp.Application.ImageService.Commands;

namespace EventsWebApp.Tests.ControllersTests
{
    public class SocialEventControllerTests
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        private CancellationTokenSource? _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        public SocialEventControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
        }


        //-------------------------------GetEventById-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetEventById_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            SocialEventDto socialEventDto = A.Fake<SocialEventDto>();
            A.CallTo(() => _mediator.Send(new GetSocialEventByIdQuery(id), _cancellationToken)).Returns(socialEventDto);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.GetEventById(id, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------GetEventByIdWithToken-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetEventByIdWithToken_ReturnsOk()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();   
            string accessToken = string.Empty;
            SocialEventDto socialEventDto = A.Fake<SocialEventDto>();
            A.CallTo(() => _mediator.Send(new GetSocialEventByUserWithTokenQuery(id, accessToken), _cancellationToken)).Returns(socialEventDto);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment) { ControllerContext = controllerContext };

            //Act
            IActionResult result = await socialEventsController.GetEventByIdWithToken(id, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------GetEventByName-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetEventByName_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            string name = "Book";
            SocialEventDto socialEventDto = A.Fake<SocialEventDto>();
            A.CallTo(() => _mediator.Send(new GetSocialEventByNameQuery(name), _cancellationToken)).Returns(socialEventDto);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.GetEventByName(name, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------GetAttendeesByEventId-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetAttendeesByEventId_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid id = Guid.NewGuid();
            List<AttendeeDto> attendees = A.Fake<List<AttendeeDto>>();
            A.CallTo(() => _mediator.Send(new GetAttendeesByEventIdQuery(id), _cancellationToken)).Returns(attendees);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.GetAttendeesByEventId(id, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------GetSocialEvents-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetSocialEvents_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            int pageIndex = 1;
            int pageSize = 10;
            AppliedFilters filters = A.Fake<AppliedFilters>();
            PaginatedList<SocialEventDto> paginatedList = new PaginatedList<SocialEventDto> { Items = new List<SocialEventDto>() };
            A.CallTo(() => _mediator.Send(new GetPaginatedSocialEventsQuery(filters, pageIndex, pageSize),_cancellationToken)).Returns(paginatedList);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.GetSocialEvents(filters, _cancellationToken,pageIndex, pageSize);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------CreateSocialEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_CreateSocialEvent_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            CreateSocialEventCommand socialEventRequest = A.Fake<CreateSocialEventCommand>();
            var socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.CreateSocialEvent(socialEventRequest, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void SocialEventControllerTests_CreateSocialEvent_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            CreateSocialEventCommand request = A.Fake<CreateSocialEventCommand>();
            var socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => socialEventsController.CreateSocialEvent(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }


        [Fact]
        public async void SocialEventControllerTests_CreateSocialEvent_ReturnsOkWithImage()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            string filePath = "..\\..\\..\\..\\EventsWebApp.Server\\wwwroot\\testing\\404383ab-3f9d-4194-8d9e-8b38136816c8.png";
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            FormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            SocialEvent socialEvent = A.Fake<SocialEvent>();
            CreateSocialEventCommand request = new CreateSocialEventCommand("a", "b", "c", "d", "f", 1,"image.png", formFile);
            A.CallTo(() => _mediator.Send(new StoreImageCommand(_webHostEnvironment.WebRootPath, request.File),_cancellationToken)).Returns(filePath);
            A.CallTo(() => _mediator.Send(request,_cancellationToken)).Returns(socialEvent.Id);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.CreateSocialEvent(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------DeleteEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_DeleteEvent_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token; 
            Guid eventId = Guid.NewGuid();
            SocialEventDto socialEventDto = new SocialEventDto { Id = eventId };
            DeleteSocialEventCommand request = new DeleteSocialEventCommand(eventId);
            A.CallTo(() => _mediator.Send(new GetSocialEventByIdQuery(request.Id), _cancellationToken)).Returns(socialEventDto);
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.Delete(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void SocialEventControllerTests_DeleteEvent_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid eventId = Guid.NewGuid();
            SocialEventDto socialEventDto = new SocialEventDto { Id = eventId };
            DeleteSocialEventCommand request = new DeleteSocialEventCommand(eventId);
            A.CallTo(() => _mediator.Send(new GetSocialEventByIdQuery(request.Id), _cancellationToken)).Returns(socialEventDto);
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => socialEventsController.Delete(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Fact]
        public async void SocialEventControllerTests_DeleteEvent_ReturnsOkWithImage()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid eventId = Guid.NewGuid();
            SocialEventDto socialEventDto = new SocialEventDto { Id = eventId, Image = "image" };
            DeleteSocialEventCommand request = new DeleteSocialEventCommand(eventId);
            A.CallTo(() => _mediator.Send(new GetSocialEventByIdQuery(request.Id), _cancellationToken)).Returns(socialEventDto);
            A.CallTo(() => _mediator.Send(new DeleteImageCommand(Path.Combine(_webHostEnvironment.WebRootPath, socialEventDto.Image)), _cancellationToken));
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.Delete(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        //-------------------------------UpdateEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_UpdateEvent_ReturnsOk()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            Guid eventId = Guid.NewGuid();
            IFormFile file = A.Fake<IFormFile>();
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            UpdateSocialEventCommand request = A.Fake<UpdateSocialEventCommand>();
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            var result = await socialEventsController.Update(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void SocialEventControllerTests_UpdateEvent_ThrowsCancellation()
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel();
            Guid eventId = Guid.NewGuid();
            IFormFile file = A.Fake<IFormFile>();
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            UpdateSocialEventCommand request = A.Fake<UpdateSocialEventCommand>();
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(() => socialEventsController.Update(request, _cancellationToken));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("The operation was canceled.");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void SocialEventControllerTests_UpdateEvent_ReturnsOkWithImage(bool hasImage)
        {
            //Arrange
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            string filePath = "..\\..\\..\\..\\EventsWebApp.Server\\wwwroot\\testing\\404383ab-3f9d-4194-8d9e-8b38136816c8.png";
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            FormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            Guid eventId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent { Image = hasImage ? "oldPath" : string.Empty };
            UpdateSocialEventCommand request = new UpdateSocialEventCommand { Image = hasImage ? "oldPath" : string.Empty, File = formFile };
            A.CallTo(() => _mediator.Send(new StoreImageCommand(_webHostEnvironment.WebRootPath, request.File), _cancellationToken)).Returns(filePath);
            A.CallTo(() => _mediator.Send(new DeleteImageCommand(Path.Combine(_webHostEnvironment.WebRootPath, request.Image)),_cancellationToken));
            A.CallTo(() => _mediator.Send(request, _cancellationToken)).Returns(eventId);
            SocialEventsController socialEventsController = new SocialEventsController(_mediator, _webHostEnvironment);

            //Act
            IActionResult result = await socialEventsController.Update(request,_cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }
    }
}
