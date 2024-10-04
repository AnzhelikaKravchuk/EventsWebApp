using AutoMapper;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("[controller]")]
    public class AtendeeController : ControllerBase
    {
        private readonly AttendeeService _attendeeService;
        private readonly IMapper _mapper;

        public AtendeeController(AttendeeService attendeeService, IMapper mapper)
        {
            _attendeeService = attendeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUser([FromQuery] Guid guid)
        {
            var attendees = await _attendeeService.GetAllAttendeesByUserId(guid);
            return Ok(attendees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendee([FromForm] CreateAttendeeRequest request,[FromQuery] Guid eventId, [FromQuery] Guid userId)
        {
            var attendee = _mapper.Map<Attendee>(request);
            attendee.DateOfRegistration = DateTime.Now.Date;
            var attendees = await _attendeeService.AddAttendeeToEvent(attendee, eventId, userId);
            return Ok(attendees);
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAttendee([FromQuery] Guid attendeeId)
        {
            var id = await _attendeeService.DeleteAttendee(attendeeId);
            return Ok(id);
        }
    }
}
