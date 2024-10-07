using AutoMapper;
using EventsWebApp.Application.Interfaces.Services;
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
        private readonly IAttendeeService _attendeeService;
        private readonly IMapper _mapper;

        public AttendeeController(IAttendeeService attendeeService, IMapper mapper)
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAttendee([FromQuery] Guid id)
        {
            var attendeeId = await _attendeeService.DeleteAttendee(id);
            return Ok(attendeeId);
        }
    }
}
