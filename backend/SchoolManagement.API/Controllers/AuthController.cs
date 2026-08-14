using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Auth;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Login with email and password</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(401)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { success = false, message = "Email and password are required." });

        var result = _authService.Login(request);

        if (!result.Success)
            return Unauthorized(new { success = false, message = result.Message });

        return Ok(result);
    }

    /// <summary>Get current authenticated user info</summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(401)]
    public IActionResult Me()
    {
        var userIdStr = User.FindFirst("userId")?.Value;
        if (!Guid.TryParse(userIdStr, out var userId))
            return Unauthorized(new { success = false, message = "Invalid token." });

        var user = _authService.GetUserById(userId);
        if (user == null)
            return NotFound(new { success = false, message = "User not found." });

        return Ok(new { success = true, data = user });
    }
}
