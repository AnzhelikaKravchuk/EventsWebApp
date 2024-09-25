using EventsWebApp.Application.Services;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IResult> Login([FromQuery] LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.Login(loginRequest.email, loginRequest.password);
                HttpContext.Response.Cookies.Append("tasty-cookies", token);
                return Results.Ok();
            }catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        [HttpPost("/register")]
        public async Task<IResult> Register([FromQuery] RegisterRequest registerRequest)
        {
            try
            {
                var token = await _userService.Register(registerRequest.email, registerRequest.password, registerRequest.username);
                HttpContext.Response.Cookies.Append("tasty-cookies", token);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}