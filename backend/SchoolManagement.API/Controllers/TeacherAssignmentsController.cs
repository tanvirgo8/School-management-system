using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.TeacherAssignments;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/teacher-assignments")]
[Authorize]
public class TeacherAssignmentsController : ControllerBase
{
    private readonly TeacherAssignmentService _service;
    public TeacherAssignmentsController(TeacherAssignmentService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Guid? teacherId)
    {
        return Ok(new { success = true, data = _service.GetAll(teacherId) });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var ta = _service.GetById(id);
        if (ta == null) return NotFound(new { success = false, message = "Assignment not found." });
        return Ok(new { success = true, data = ta });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create([FromBody] CreateTeacherAssignmentRequest req)
    {
        var (result, error) = _service.Create(req);
        if (error != null) return BadRequest(new { success = false, message = error });
        return Created($"/api/teacher-assignments/{result!.Id}", new { success = true, data = result });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(Guid id)
    {
        if (!_service.Delete(id)) return NotFound(new { success = false, message = "Assignment not found." });
        return Ok(new { success = true, message = "Teacher assignment removed." });
    }
}
