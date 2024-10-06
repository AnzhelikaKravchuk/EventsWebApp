using AutoMapper;
using EventsWebApp.Application.Services;
using EventsWebApp.Domain.Models;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Authorize("User")]
    [Authorize("Admin")]
    [Route("[controller]")]
    public class AttendeeController : ControllerBase    
    {
        private readonly AttendeeService _attendeeService;
        private readonly IMapper _mapper;

        public AttendeeController(AttendeeService attendeeService, IMapper mapper)
        {
            _attendeeService = attendeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllByUser()
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var attendees = await _attendeeService.GetAttendeesByToken(accessToken);

            var responseList = attendees.Select(_mapper.Map<AttendeeResponse>).ToList();
            return Ok(responseList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendee(CreateAttendeeRequest request, [FromQuery] Guid eventId)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var attendee = _mapper.Map<Attendee>(request);
            attendee.DateOfRegistration = DateTime.Now.Date;
            var attendees = await _attendeeService.AddAttendeeToEventWithToken(attendee, eventId, accessToken);
            return Ok(attendees);
        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteAttendee([FromQuery] Guid attendeeId)
        {
            var id = await _attendeeService.DeleteAttendee(attendeeId);
            return Ok(id);
        }
    }
}
