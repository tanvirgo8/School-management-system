using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Users;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService) => _userService = userService;

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public IActionResult GetAll([FromQuery] string? role, [FromQuery] string? search)
    {
        var users = _userService.GetAll(role, search);
        return Ok(new { success = true, data = users });
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult GetById(Guid id)
    {
        var user = _userService.GetById(id);
        if (user == null) return NotFound(new { success = false, message = "User not found." });
        return Ok(new { success = true, data = user });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create([FromBody] CreateUserRequest req)
    {
        var (user, error) = _userService.Create(req);
        if (error != null) return Conflict(new { success = false, message = error });
        return Created($"/api/users/{user!.Id}", new { success = true, data = user });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Update(Guid id, [FromBody] UpdateUserRequest req)
    {
        var (user, error) = _userService.Update(id, req);
        if (error == "User not found.") return NotFound(new { success = false, message = error });
        if (error != null) return Conflict(new { success = false, message = error });
        return Ok(new { success = true, data = user });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _userService.Delete(id);
        if (!deleted) return NotFound(new { success = false, message = "User not found." });
        return Ok(new { success = true, message = "User deleted successfully." });
    }
}
