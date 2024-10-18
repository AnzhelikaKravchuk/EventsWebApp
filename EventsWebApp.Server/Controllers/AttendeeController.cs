using AutoMapper;
using EventsWebApp.Application.Interfaces.Services;
using EventsWebApp.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendeeController : ControllerBase    
    {
        private readonly IAttendeeService _attendeeService;
        private readonly IMapper _mapper;

        public AttendeeController(IAttendeeService attendeeService, IMapper mapper)
        {
            _attendeeService = attendeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllByUser(CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var attendees = await _attendeeService.GetAttendeesByToken(accessToken, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            var responseList = attendees.Select(_mapper.Map<AttendeeResponse>).ToList();
            return Ok(responseList);
        }

        [HttpGet("getAttendeeById")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var attendee = await _attendeeService.GetAttendeeById(id, cancellationToken);
            return Ok(attendee);
        }

        [HttpDelete]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAttendee([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var attendeeId = await _attendeeService.DeleteAttendee(id,cancellationToken);
            return Ok(attendeeId);
        }
    }
}
