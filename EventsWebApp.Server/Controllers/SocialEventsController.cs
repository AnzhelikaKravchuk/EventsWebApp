using AutoMapper;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocialEventsController : ControllerBase { 
        private readonly SocialEventService _socialEventService;
        private readonly IMapper _mapper;

        public SocialEventsController(SocialEventService socialEventService, IMapper mapper)
        {
            _socialEventService = socialEventService;
            _mapper = mapper;
        }

        [HttpGet("socialEvents")]
        public async Task<IActionResult> GetAll()
        {
            var socialEvents = await _socialEventService.GetAllSocialEvents();
            var listResponse = socialEvents.Select(s => _mapper.Map<SocialEventResponse>(s)).ToList();
            return Ok(listResponse);
        }

        [HttpPost("createEvent")]
        public async Task<IActionResult> Create([FromForm] CreateSocialEventRequest request, [FromForm] IFormFileCollection formFiles)
        {
            
            var socialEvent = _mapper.Map<SocialEvent>(request);
            if (formFiles.Count > 0 && formFiles[0].IsImage())
            {
                byte[] image = ConvertFileToBytes(formFiles[0]);
                socialEvent.Image = image;
            }


            var socialEvents = await _socialEventService.CreateSocialEvent(socialEvent);
            return Ok(socialEvents);
        }

        [HttpDelete("deleteEvent")]
        public async Task<IActionResult> Delete([FromBody] Guid guid)
        {
            await _socialEventService.DeleteSocialEvent(guid);
            return Ok();
        }

        [HttpPut("updateEvent")]
        public async Task<IActionResult> Update([FromForm] UpdateSocialEventRequest request)
        {
            var socialEvent = _mapper.Map<SocialEvent>(request);
            await _socialEventService.UpdateSocialEvent(socialEvent);
            return Ok();
        }

        private byte[] ConvertFileToBytes(IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
            
        }
    }

}