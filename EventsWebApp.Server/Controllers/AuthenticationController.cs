using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using EventsWebApp.Application.Users.Commands.RegisterUserCommand;
using EventsWebApp.Application.Users.Commands.LoginUserCommand;
using EventsWebApp.Application.Users.Queries.GetRoleByTokenQuery;
using EventsWebApp.Application.Users.Commands.RefreshTokenCommand;
namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginUserCommand loginRequest, CancellationToken cancellationToken)
        {
            var (accessToken, refreshToken) = await _mediator.Send(loginRequest, cancellationToken);
            HttpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions { Domain = "localhost" });
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions { Domain = "localhost" });
            return Ok((accessToken, refreshToken));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
        {
            var (accessToken, refreshToken) = await _mediator.Send(registerUserCommand, cancellationToken);
            HttpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions { Domain ="localhost"});
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions { Domain = "localhost" });
            return Ok((accessToken, refreshToken));
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["accessToken"] != null)
            {
                HttpContext.Response.Cookies.Append("accessToken", "", new CookieOptions { Domain = "localhost", Expires = DateTime.Now.AddDays(-1) });
            }

            if (HttpContext.Request.Cookies["refreshToken"] != null)
            {
                HttpContext.Response.Cookies.Append("refreshToken", "", new CookieOptions { Domain = "localhost", Expires = DateTime.Now.AddDays(-1) });
            }
            return Ok();
        }

        [HttpGet("/getRole")]
        [Authorize]
        public IActionResult GetRole(CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];

            cancellationToken.ThrowIfCancellationRequested();
            var role = accessToken.IsNullOrEmpty() ? null : _mediator.Send(new GetRoleByTokenQuery(accessToken), cancellationToken).Result;

            return Ok(role);
        }

        [HttpPost("/refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            var accessToken = HttpContext.Request.Cookies["accessToken"];
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];

            cancellationToken.ThrowIfCancellationRequested();
            accessToken = await _mediator.Send(new RefreshTokenCommand(accessToken, refreshToken), cancellationToken);
            
            HttpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions { Domain = "localhost" });
            return Ok((accessToken, refreshToken));
        }
    }
}