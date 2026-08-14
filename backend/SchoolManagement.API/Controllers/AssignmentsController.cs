using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Assignments;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/assignments")]
[Authorize]
public class AssignmentsController : ControllerBase
{
    private readonly AssignmentService _service;
    public AssignmentsController(AssignmentService service) => _service = service;

    private Guid CurrentUserId => Guid.Parse(User.FindFirst("userId")!.Value);
    private string CurrentRole => User.FindFirst("role")?.Value ?? "";
    private bool IsAdmin => CurrentRole == "ADMIN";
    private bool IsTeacher => CurrentRole == "TEACHER";

    [HttpGet]
    public IActionResult GetAll([FromQuery] Guid? teacherId, [FromQuery] Guid? classId, [FromQuery] string? status)
    {
        if (IsTeacher)
            return Ok(new { success = true, data = _service.GetAll(teacherId: CurrentUserId, classId, status) });

        return Ok(new { success = true, data = _service.GetAll(teacherId, classId, status) });
    }

    /// <summary>Get assignments for the currently logged-in student</summary>
    [HttpGet("my")]
    [Authorize(Roles = "STUDENT")]
    public IActionResult GetMyAssignments()
    {
        // classId is not in the token, so we use the UserService via DI — but to keep it simple,
        // we pass classId in query param from frontend (the frontend reads it from the /auth/me response)
        return BadRequest(new { success = false, message = "Use /api/assignments?classId={classId} instead." });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var assignment = _service.GetById(id);
        if (assignment == null) return NotFound(new { success = false, message = "Assignment not found." });

        // Students can only see PUBLISHED assignments
        if (CurrentRole == "STUDENT" && assignment.Status != "PUBLISHED")
            return NotFound(new { success = false, message = "Assignment not found." });

        return Ok(new { success = true, data = assignment });
    }

    [HttpPost]
    [Authorize(Roles = "TEACHER,ADMIN")]
    public IActionResult Create([FromBody] CreateAssignmentRequest req)
    {
        var teacherId = IsAdmin ? req.TeacherId != Guid.Empty ? req.TeacherId : CurrentUserId : CurrentUserId;
        var (result, error) = _service.Create(req, teacherId);
        if (error != null) return BadRequest(new { success = false, message = error });
        return Created($"/api/assignments/{result!.Id}", new { success = true, data = result });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "TEACHER,ADMIN")]
    public IActionResult Update(Guid id, [FromBody] UpdateAssignmentRequest req)
    {
        var (result, error) = _service.Update(id, req, CurrentUserId, IsAdmin);
        if (error?.Contains("not found") == true) return NotFound(new { success = false, message = error });
        if (error != null) return BadRequest(new { success = false, message = error });
        return Ok(new { success = true, data = result });
    }

    [HttpPost("{id:guid}/publish")]
    [Authorize(Roles = "TEACHER,ADMIN")]
    public IActionResult Publish(Guid id)
    {
        var (result, error) = _service.Publish(id, CurrentUserId, IsAdmin);
        if (error?.Contains("not found") == true) return NotFound(new { success = false, message = error });
        if (error != null) return BadRequest(new { success = false, message = error });
        return Ok(new { success = true, data = result, message = "Assignment published successfully." });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "TEACHER,ADMIN")]
    public IActionResult Delete(Guid id)
    {
        var (success, error) = _service.Delete(id, CurrentUserId, IsAdmin);
        if (error?.Contains("not found") == true) return NotFound(new { success = false, message = error });
        if (error != null) return BadRequest(new { success = false, message = error });
        return Ok(new { success = true, message = "Assignment deleted successfully." });
    }
}
