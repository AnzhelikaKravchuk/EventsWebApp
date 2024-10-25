using EventsWebApp.Domain.Filters;
using EventsWebApp.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.UseCases.SocialEvents.Queries;
using EventsWebApp.Application.UseCases.SocialEvents.Commands;
using EventsWebApp.Application.UseCases.ImageService.Commands;

namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocialEventsController : ControllerBase {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SocialEventsController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getSocialEventByIdWithToken")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventByIdWithToken([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Request.Cookies["accessToken"];
            SocialEventDto socialEvent;
            socialEvent = await _mediator.Send( new GetSocialEventByUserWithTokenQuery(id, accessToken), cancellationToken);
            return Ok(socialEvent);
        }

        [HttpGet("getSocialEventById")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventById([FromQuery] GetSocialEventByIdQuery request, CancellationToken cancellationToken)
        {
            SocialEventDto socialEvent = await _mediator.Send(request, cancellationToken);
            return Ok(socialEvent);
        }

        [HttpGet("getSocialEventByName")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetEventByName([FromQuery] GetSocialEventByNameQuery request, CancellationToken cancellationToken)
        {
            SocialEventDto socialEvent = await _mediator.Send(request, cancellationToken);
            return Ok(socialEvent);
        }

        [HttpGet("getAttendeesByEventId")]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAttendeesByEventId([FromQuery] GetAttendeesByEventIdQuery request, CancellationToken cancellationToken)
        {
            List<AttendeeDto> attendees = await _mediator.Send(request, cancellationToken);

            return Ok(attendees);
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetSocialEvents([FromForm] AppliedFilters filters, CancellationToken cancellationToken, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            PaginatedList<SocialEventDto> socialEvents = await _mediator.Send(new GetPaginatedSocialEventsQuery(filters, pageIndex, pageSize), cancellationToken);
            
            return Ok(socialEvents);
        }

        [HttpPost("createEvent")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> CreateSocialEvent([FromForm] CreateSocialEventCommand request, CancellationToken cancellationToken)
        {
            if (request.File != null && request.File.IsImage())
            {
                cancellationToken.ThrowIfCancellationRequested();
                string newPath = await _mediator.Send(new StoreImageCommand(_webHostEnvironment.WebRootPath, request.File), cancellationToken);
                request.Image = newPath;
            }
            cancellationToken.ThrowIfCancellationRequested();
            Guid id = await _mediator.Send(request, cancellationToken);
            return Ok(id);
        }


        [HttpDelete("deleteEvent")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DeleteSocialEventCommand request, CancellationToken cancellationToken)
        {
            SocialEventDto socialEvent = await _mediator.Send(new GetSocialEventByIdQuery(request.Id), cancellationToken);
            if (!socialEvent.Image.IsNullOrEmpty())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _mediator.Send(new DeleteImageCommand(Path.Combine(_webHostEnvironment.WebRootPath, socialEvent.Image)));
            }
            cancellationToken.ThrowIfCancellationRequested();
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPut("updateEvent")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update([FromForm] UpdateSocialEventCommand request, CancellationToken cancellationToken)
        {
            if (request.File != null && request.File.IsImage())
            {
                string newPath = await _mediator.Send(new StoreImageCommand(_webHostEnvironment.WebRootPath, request.File), cancellationToken);
                if (!request.Image.IsNullOrEmpty())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await _mediator.Send(new DeleteImageCommand(Path.Combine(_webHostEnvironment.WebRootPath, request.Image)));
                }
                request.Image = newPath;
            }
            cancellationToken.ThrowIfCancellationRequested();
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
    }

}
