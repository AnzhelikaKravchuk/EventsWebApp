using EventsWebApp.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventsWebApp.Application.Attendees.Commands;
using EventsWebApp.Application.Validators;
using EventsWebApp.Application.Attendees.Queries;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllByUser(CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Request.Cookies["accessToken"];
            List<AttendeeDto> attendees = await _mediator.Send(new GetAttendeesWithTokenQuery(accessToken), cancellationToken);
            return Ok(attendees);
        }

        [HttpGet("getAttendeeById")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById([FromQuery] GetAttendeeByIdQuery request, CancellationToken cancellationToken)
        {
            AttendeeDto attendee = await _mediator.Send(request, cancellationToken);
            return Ok(attendee);
        }

        [HttpPost("addAttendee")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAttendee(AddUpdateAttendeeRequest request, [FromQuery] Guid eventId, CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Request.Cookies["accessToken"];
            cancellationToken.ThrowIfCancellationRequested();
            Guid resultId = await _mediator.Send(new AddAttendeeWithTokenCommand(request.Name, request.Surname, request.Email, request.DateOfBirth, eventId, accessToken), cancellationToken);
            return Ok(resultId);
        }

        [HttpDelete]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAttendee([FromQuery] DeleteAttendeeCommand request, CancellationToken cancellationToken)
        {
            Guid attendeeId = await _mediator.Send(request, cancellationToken);
            return Ok(attendeeId);
        }
    }
}
