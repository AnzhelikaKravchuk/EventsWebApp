using AutoMapper;
using EventsWebApp.Domain.Filters;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using EventsWebApp.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
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

        [HttpGet("getSocialEventByIdWithToken")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventByIdWithToken([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            SocialEvent socialEvent;
            bool isAlreadyInList;
            (socialEvent, isAlreadyInList) = await _socialEventService.GetSocialEventByIdWithToken(id, accessToken, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            var socialEventResponse = _mapper.Map<SocialEventResponse>(socialEvent);
            socialEventResponse.IsAlreadyInList = isAlreadyInList;
            return Ok(socialEventResponse);
        }

        [HttpGet("getSocialEventById")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventById([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var socialEvent = await _socialEventService.GetSocialEventById(id, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            var socialEventResponse = _mapper.Map<SocialEventResponse>(socialEvent);
            return Ok(socialEventResponse);
        }

        [HttpGet("getSocialEventByName")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var socialEvent = await _socialEventService.GetSocialEventByName(name, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            var socialEventResponse = _mapper.Map<SocialEventResponse>(socialEvent);
            return Ok(socialEventResponse);
        }

        [HttpGet("getAttendeesByEventId")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAttendeesByEventId([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var attendees = await _socialEventService.GetAttendeesById(id, cancellationToken);

            return Ok(attendees);
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetSocialEvents([FromForm] AppliedFilters filters, CancellationToken cancellationToken, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var socialEvents = await _socialEventService.GetSocialEvents(filters, pageIndex, pageSize, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            var responseList = new PaginatedList<SocialEventResponse>(null, socialEvents.PageIndex, socialEvents.TotalPages);
            responseList.Items = socialEvents.Items.ConvertAll(_mapper.Map<SocialEventResponse>).ToList();
            return Ok(responseList);
        }

        [HttpPost("createEvent")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> CreateSocialEvent([FromForm] CreateSocialEventRequest request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<SocialEvent>(request); 
            if (request.File != null && request.File.IsImage())
            {
                cancellationToken.ThrowIfCancellationRequested();
                string newPath = await _imageService.StoreImage(_webHostEnvironment.WebRootPath, request.File);
                data.Image = newPath;
            }
            cancellationToken.ThrowIfCancellationRequested();
            var id = await _socialEventService.CreateSocialEvent(data, cancellationToken);
            return Ok(id);
        }

        [HttpPost("addAttendee")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAttendee(CreateAttendeeRequest request, [FromQuery] Guid eventId, CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var attendee = _mapper.Map<Attendee>(request);
            attendee.DateOfRegistration = DateTime.Now.Date;
            cancellationToken.ThrowIfCancellationRequested();
            var resultId = await _socialEventService.AddAttendeeToEventWithToken(eventId, attendee, accessToken, cancellationToken);
            return Ok(resultId);
        }

        [HttpDelete("deleteEvent")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var socialEvent = await _socialEventService.GetSocialEventById(id, cancellationToken);
            if (!socialEvent.Image.IsNullOrEmpty())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _imageService.DeleteImage(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image));
            }
            cancellationToken.ThrowIfCancellationRequested();
            await _socialEventService.DeleteSocialEvent(id, cancellationToken);
            return Ok();
        }

        [HttpPut("updateEvent")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update([FromForm] UpdateSocialEventRequest request, CancellationToken cancellationToken)
        {
            var socialEvent = _mapper.Map<SocialEvent>(request);
            if (request.File != null && request.File.IsImage())
            {
                string newPath = await _imageService.StoreImage(_webHostEnvironment.WebRootPath, request.File);
                if (!socialEvent.Image.IsNullOrEmpty())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await _imageService.DeleteImage(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image));
                }
                socialEvent.Image = newPath;
            }
            cancellationToken.ThrowIfCancellationRequested();
            await _socialEventService.UpdateSocialEvent(socialEvent, cancellationToken);
            return Ok();
        }
    }

}
