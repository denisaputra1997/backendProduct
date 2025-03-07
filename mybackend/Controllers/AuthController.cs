using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using mybackend.Models;
using mybackend.Services;

namespace mybackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    [EnableCors("AllowAllOrigins")]
    public IActionResult Login([FromBody] User user)
    {
        var token = _authService.Authenticate(user.Username, user.Password);
        if (token == null)
        {
            return Unauthorized();
        }
        else
        {
            var userData = _authService.GetUserByUsername(user.Username);
            return Ok(new { token, user = userData });
        }

    }

    // [HttpGet("user")]
    // [Authorize]
    // public IActionResult GetUserData()
    // {
    //     var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     if (username == null)
    //     {
    //         return Unauthorized();
    //     }

    //     var user = _authService.GetUserByUsername(username);
    //     return Ok(user);
    // }
}

