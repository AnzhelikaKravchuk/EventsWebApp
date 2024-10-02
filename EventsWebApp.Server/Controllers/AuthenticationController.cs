using EventsWebApp.Application.Services;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            var (accessToken, refreshToken) = await _userService.Login(loginRequest.email, loginRequest.password);
            HttpContext.Response.Cookies.Append("tasty-cookies", accessToken);
            HttpContext.Response.Cookies.Append("very-tasty-cookies", refreshToken);
            return Ok();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
        {
            var (accessToken,refreshToken) = await _userService.Register(registerRequest.email, registerRequest.password, registerRequest.username);
            HttpContext.Response.Cookies.Append("tasty-cookies", accessToken);
            HttpContext.Response.Cookies.Append("very-tasty-cookies", refreshToken);
            return Ok();
        }

        [HttpPost("/refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            var accessToken = HttpContext.Request.Cookies.First(c => c.Key == "tasty-cookies").Value;
            var refreshToken = HttpContext.Request.Cookies.First(c => c.Key == "very-tasty-cookies").Value;

            (accessToken, refreshToken) = await _userService.RefreshToken(accessToken, refreshToken);
            
            HttpContext.Response.Cookies.Append("tasty-cookies", accessToken);
            HttpContext.Response.Cookies.Append("very-tasty-cookies", refreshToken);
            return Ok();
        }
    }
}