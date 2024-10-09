using EventsWebApp.Application.Interfaces.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Controllers;
using FluentAssertions;
using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.PaginationHandlers;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Tests.ControllersTests
{
    public class SocialEventControllerTests
    {
        private readonly ISocialEventService _socialEventService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SocialEventControllerTests()
        {
            _socialEventService = A.Fake<ISocialEventService>();
            _imageService = A.Fake<IImageService>();
            _mapper = A.Fake<IMapper>();
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
        }


        //-------------------------------GetEventById-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetEventById_ReturnsOk()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            A.CallTo(()=>_socialEventService.GetSocialEventById(id)).Returns(socialEvent);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.GetEventById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------GetEventByName-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetEventByName_ReturnsOk()
        {
            //Arrange
            string name = "Book";
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            A.CallTo(() => _socialEventService.GetSocialEventByName(name)).Returns(socialEvent);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.GetEventByName(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        //-------------------------------GetAttendeesByEventId-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetAttendeesByEventId_ReturnsOk()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var attendees = A.Fake<List<Attendee>>();
            A.CallTo(() => _socialEventService.GetAttendeesById(id)).Returns(attendees);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.GetAttendeesByEventId(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------GetSocialEvents-----------------------
        [Fact]
        public async void SocialEventControllerTests_GetSocialEvents_ReturnsOk()
        {
            //Arrange
            int pageIndex = 1;
            int pageSize = 10;
            AppliedFilters filters = A.Fake<AppliedFilters>();
            var paginatedList = new PaginatedList<SocialEvent> { Items = new List<SocialEvent>()};
            SocialEventResponse socialEventResponse = A.Fake<SocialEventResponse>();
            A.CallTo(() => _socialEventService.GetSocialEvents(filters, pageIndex, pageSize)).Returns(paginatedList);
            A.CallTo(() => _mapper.Map<SocialEventResponse>(A<SocialEvent>.Ignored)).Returns(socialEventResponse);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.GetSocialEvents(filters, pageIndex, pageSize);

            //Assert
            result.Should().NotBeNull();
            var okResult =result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------CreateSocialEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_CreateSocialEvent_ReturnsOk()
        {
            //Arrange
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            CreateSocialEventRequest socialEventRequest = A.Fake<CreateSocialEventRequest>();
            A.CallTo(() => _mapper.Map<SocialEvent>(socialEventRequest)).Returns(socialEvent);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.CreateSocialEvent(socialEventRequest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async void SocialEventControllerTests_CreateSocialEvent_ReturnsOkWithImage()
        {
            //Arrange
            string filePath = "..\\..\\..\\..\\EventsWebApp.Server\\wwwroot\\testing\\404383ab-3f9d-4194-8d9e-8b38136816c8.png";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg" 
            };

            SocialEvent socialEvent = A.Fake<SocialEvent>();
            CreateSocialEventRequest socialEventRequest = new CreateSocialEventRequest("a", "b", "c", "d", "f", 1, formFile);
            A.CallTo(() => _mapper.Map<SocialEvent>(socialEventRequest)).Returns(socialEvent);
            A.CallTo(() => _imageService.StoreImage(_webHostEnvironment.WebRootPath, socialEventRequest.File)).Returns(filePath);
            A.CallTo(() => _socialEventService.CreateSocialEvent(socialEvent)).Returns(socialEvent.Id);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.CreateSocialEvent(socialEventRequest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------AddAttendee-----------------------
        [Fact]
        public async void SocialEventControllerTests_AddAttendee_ReturnsOk()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            Guid resultId = Guid.NewGuid();
            string accessToken = "Access-token";
            Attendee attendee = A.Fake<Attendee>();
            CreateAttendeeRequest request = A.Fake<CreateAttendeeRequest>();
            A.CallTo(() => _mapper.Map<Attendee>(request)).Returns(attendee);
            A.CallTo(() => _socialEventService.AddAttendeeToEventWithToken(eventId, attendee, accessToken)).Returns(resultId);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);
            socialEventsController.ControllerContext.HttpContext = new DefaultHttpContext();

            //Act
            var result = await socialEventsController.AddAttendee(request, eventId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }


        //-------------------------------DeleteEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_DeleteEvent_ReturnsOk()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            A.CallTo(() => _socialEventService.DeleteSocialEvent(eventId)).Returns(eventId);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.Delete(eventId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void SocialEventControllerTests_DeleteEvent_ReturnsOkWithImage()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent { Image ="image"};
            A.CallTo(() => _socialEventService.GetSocialEventById(eventId)).Returns(socialEvent);
            A.CallTo(() => _imageService.DeleteImage(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image)));
            A.CallTo(() => _socialEventService.DeleteSocialEvent(eventId)).Returns(eventId);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.Delete(eventId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        //-------------------------------UpdateEvent-----------------------
        [Fact]
        public async void SocialEventControllerTests_UpdateEvent_ReturnsOk()
        {
            //Arrange
            Guid eventId = Guid.NewGuid();
            IFormFile file = A.Fake<IFormFile>();
            SocialEvent socialEvent = A.Fake<SocialEvent>();
            UpdateSocialEventRequest updateSocialEventRequest = A.Fake<UpdateSocialEventRequest>();
            A.CallTo(() => _socialEventService.GetSocialEventById(eventId)).Returns(socialEvent);
            A.CallTo(() => _socialEventService.UpdateSocialEvent(socialEvent)).Returns(eventId);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.Update(updateSocialEventRequest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void SocialEventControllerTests_UpdateEvent_ReturnsOkWithImage(bool hasImage)
        {
            //Arrange
            string filePath = "..\\..\\..\\..\\EventsWebApp.Server\\wwwroot\\testing\\404383ab-3f9d-4194-8d9e-8b38136816c8.png";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            Guid eventId = Guid.NewGuid();
            SocialEvent socialEvent = new SocialEvent { Image = hasImage ? "oldPath": string.Empty };
            UpdateSocialEventRequest updateSocialEventRequest = A.Fake<UpdateSocialEventRequest>();
            A.CallTo(() => _socialEventService.GetSocialEventById(eventId)).Returns(socialEvent);
            A.CallTo(() => _imageService.StoreImage(_webHostEnvironment.WebRootPath, formFile)).Returns(filePath);
            A.CallTo(() => _imageService.DeleteImage(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image)));
            A.CallTo(() => _socialEventService.UpdateSocialEvent(socialEvent)).Returns(eventId);
            var socialEventsController = new SocialEventsController(_socialEventService, _imageService, _mapper, _webHostEnvironment);

            //Act
            var result = await socialEventsController.Update(updateSocialEventRequest);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }
    }
}
