using EventsWebApp.Application.Services;
using EventsWebApp.Server.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace EventsWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/login")]
        public async Task<IResult> Login([FromQuery] LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.Login(loginRequest.email, loginRequest.password);
                return Results.Ok(token);
            }catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        [HttpGet("/register")]
        public async Task<IResult> Register([FromQuery] RegisterRequest registerRequest)
        {
            try
            {
                await _userService.Register(registerRequest.email, registerRequest.password, registerRequest.username) ;
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}