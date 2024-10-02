using AutoMapper;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.PaginationHandlers;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Authorize(Policy = "UserPolicy")]
    [Route("[controller]")]
    public class SocialEventsController : ControllerBase { 
        private readonly SocialEventService _socialEventService;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SocialEventsController(SocialEventService socialEventService, ImageService imageService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
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


        [HttpGet("getSocialEventByName")]
        public async Task<IActionResult> GetEventByName([FromQuery] string name)
        {
            var socialEvents = await _socialEventService.GetSocialEventsByName(name);

            return Ok(socialEvents);
        }

        [HttpGet("getSocialEventByDate")]
        public async Task<IActionResult> GetEventByDate([FromQuery] string date)
        {
            var socialEvents = await _socialEventService.GetSocialEventsByDate(DateTime.Parse(date));

            return Ok(socialEvents);
        }

        [HttpGet("getSocialEventByCategory")]
        public async Task<IActionResult> GetEventByCategory([FromQuery] string category)
        {
            var socialEvents = await _socialEventService.GetSocialEventsByCategory((E_SocialEventCategory)Enum.Parse(typeof(E_SocialEventCategory), category));

            return Ok(socialEvents);
        }


        [HttpGet("getSocialEventByPlace")]
        public async Task<IActionResult> GetEventByPlace([FromQuery] string place)
        {
            var socialEvents = await _socialEventService.GetSocialEventsByPlace(place);

            return Ok(socialEvents);
        }

        [HttpGet]
        public async Task<IActionResult> GetSocialEvents([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var socialEvents = await _socialEventService.GetAllSocialEvents(pageIndex, pageSize);
            var responseList = new PaginatedList<SocialEventResponse>(null, socialEvents.PageIndex, socialEvents.TotalPages);
            responseList.Items = socialEvents.Items.Select(s => _mapper.Map<SocialEventResponse>(s)).ToList();
            return Ok(responseList);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateSocialEvent([FromForm] CreateSocialEventRequest request, IFormFile formFile)
        {
            
            var socialEvent = _mapper.Map<SocialEvent>(request);
            if (formFile != null && formFile.IsImage())
            {
                socialEvent.Image = await _imageService.StoreImage(_webHostEnvironment.WebRootPath, formFile);
            }

            var socialEvents = await _socialEventService.CreateSocialEvent(socialEvent);
            return Ok(socialEvents);
        }

        [HttpDelete("deleteEvent")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete([FromBody] Guid guid)
        {
            await _socialEventService.DeleteSocialEvent(guid);
            return Ok();
        }

        [HttpPut("updateEvent")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Update([FromForm] UpdateSocialEventRequest request)
        {
            var socialEvent = _mapper.Map<SocialEvent>(request);
            await _socialEventService.UpdateSocialEvent(socialEvent);
            return Ok();
        }

    }

}
