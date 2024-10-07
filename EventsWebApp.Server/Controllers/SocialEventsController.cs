using AutoMapper;
using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Authorize("User")]
    [Authorize("Admin")]
    [Route("[controller]")]
    public class SocialEventsController : ControllerBase { 
        private readonly ISocialEventService _socialEventService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SocialEventsController(ISocialEventService socialEventService, IImageService imageService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _socialEventService = socialEventService;
            _imageService = imageService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getSocialEventById")]
        public async Task<IActionResult> GetEventById([FromQuery] Guid id)
        {
            var socialEvent = await _socialEventService.GetSocialEventById(id);

            return Ok(socialEvent);
        }


        [HttpGet("getAttendeesByEventId")]
        public async Task<IActionResult> GetAttendeesByEventId([FromQuery] Guid id)
        {
            var attendees = await _socialEventService.GetAttendeesById(id);

            return Ok(attendees);
        }

        [HttpPost]
        public async Task<IActionResult> GetSocialEvents([FromForm] AppliedFilters filters, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var socialEvents = await _socialEventService.GetSocialEvents(filters,pageIndex, pageSize);
            var responseList = new PaginatedList<SocialEventResponse>(null, socialEvents.PageIndex, socialEvents.TotalPages);
            responseList.Items = socialEvents.Items.Select(s => _mapper.Map<SocialEventResponse>(s)).ToList();
            return Ok(responseList);
        }

        [HttpPost("createEvent")]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateSocialEvent([FromForm] CreateSocialEventRequest request)
        {
            var data = _mapper.Map<SocialEvent>(request); 
            if (request.File != null && request.File.IsImage())
            {
                string newPath = await _imageService.StoreImage(_webHostEnvironment.WebRootPath, request.File);
                data.Image = newPath;
            }
            var id = await _socialEventService.CreateSocialEvent(data);
            return Ok(id);
        }

        [HttpPost("addAttendee")]
        public async Task<IActionResult> AddAttendee(CreateAttendeeRequest request, [FromQuery] Guid eventId)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var attendee = _mapper.Map<Attendee>(request);
            attendee.DateOfRegistration = DateTime.Now.Date;
            var resultId = await _socialEventService.AddAttendeeToEventWithToken(eventId, attendee, accessToken);
            return Ok(resultId);
        }

        [HttpDelete("deleteEvent")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _socialEventService.DeleteSocialEvent(id);
            return Ok();
        }

        [HttpPut("updateEvent")]
        [Authorize("Admin")]
        public async Task<IActionResult> Update( UpdateSocialEventRequest request)
        {
            var socialEvent = _mapper.Map<SocialEvent>(request);
            await _socialEventService.UpdateSocialEvent(socialEvent);
            return Ok();
        }

        [HttpPut("upload")]
        [Authorize("Admin")]
        public async Task<IActionResult> Upload([FromQuery] Guid id, [FromForm] IFormFile formFile)
        {
            var socialEvent = await _socialEventService.GetSocialEventById(id);
            if (formFile != null && formFile.IsImage())
            {
               string newPath = await _imageService.StoreImage(_webHostEnvironment.WebRootPath, formFile);
                if (!socialEvent.Image.IsNullOrEmpty())
                {
                    await _imageService.DeleteImage(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image));
                }
                socialEvent.Image = newPath;
            }
            await _socialEventService.UpdateSocialEvent(socialEvent);
            return Ok();
        }

    }

}
