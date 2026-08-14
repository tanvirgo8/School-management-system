using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Submissions;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/submissions")]
[Authorize]
public class SubmissionsController : ControllerBase
{
    private readonly SubmissionService _service;
    public SubmissionsController(SubmissionService service) => _service = service;

    private Guid CurrentUserId => Guid.Parse(User.FindFirst("userId")!.Value);
    private string CurrentRole => User.FindFirst("role")?.Value ?? "";
    private bool IsAdmin => CurrentRole == "ADMIN";
    private bool IsTeacher => CurrentRole == "TEACHER";

    [HttpGet]
    public IActionResult GetAll([FromQuery] Guid? assignmentId, [FromQuery] Guid? studentId)
    {
        if (CurrentRole == "STUDENT")
            return Ok(new { success = true, data = _service.GetAll(assignmentId, studentId: CurrentUserId) });

        if (IsTeacher)
            return Ok(new { success = true, data = _service.GetAll(assignmentId, studentId, teacherId: CurrentUserId) });

        // Admin: all
        return Ok(new { success = true, data = _service.GetAll(assignmentId, studentId) });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var sub = _service.GetById(id,
            requestingUserId: CurrentUserId,
            isAdmin: IsAdmin,
            isTeacher: IsTeacher);

        if (sub == null) return NotFound(new { success = false, message = "Submission not found." });
        return Ok(new { success = true, data = sub });
    }

    [HttpPost]
    [Authorize(Roles = "STUDENT")]
    public IActionResult Create([FromBody] CreateSubmissionRequest req)
    {
        var (result, error) = _service.Create(req, CurrentUserId);
        if (error != null) return BadRequest(new { success = false, message = error });
        return Created($"/api/submissions/{result!.Id}", new { success = true, data = result, message = "Assignment submitted successfully." });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "STUDENT")]
    public IActionResult Update(Guid id, [FromBody] UpdateSubmissionRequest req)
    {
        var (result, error) = _service.Update(id, req, CurrentUserId);
        if (error?.Contains("not found") == true) return NotFound(new { success = false, message = error });
        if (error != null) return BadRequest(new { success = false, message = error });
        return Ok(new { success = true, data = result, message = "Submission updated successfully." });
    }

    [HttpPost("{id:guid}/grade")]
    [Authorize(Roles = "TEACHER,ADMIN")]
    public IActionResult Grade(Guid id, [FromBody] GradeSubmissionRequest req)
    {
        var (result, error) = _service.Grade(id, req, CurrentUserId, IsAdmin);
        if (error?.Contains("not found") == true) return NotFound(new { success = false, message = error });
        if (error != null) return BadRequest(new { success = false, message = error });
        return Ok(new { success = true, data = result, message = "Submission graded successfully." });
    }
}
