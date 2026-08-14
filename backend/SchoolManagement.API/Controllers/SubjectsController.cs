using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Subjects;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/subjects")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly SubjectService _subjectService;
    public SubjectsController(SubjectService subjectService) => _subjectService = subjectService;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Guid? classId)
    {
        return Ok(new { success = true, data = _subjectService.GetAll(classId) });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var s = _subjectService.GetById(id);
        if (s == null) return NotFound(new { success = false, message = "Subject not found." });
        return Ok(new { success = true, data = s });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create([FromBody] CreateSubjectRequest req)
    {
        var (s, error) = _subjectService.Create(req);
        if (error != null) return Conflict(new { success = false, message = error });
        return Created($"/api/subjects/{s!.Id}", new { success = true, data = s });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Update(Guid id, [FromBody] UpdateSubjectRequest req)
    {
        var (s, error) = _subjectService.Update(id, req);
        if (error == "Subject not found.") return NotFound(new { success = false, message = error });
        if (error != null) return Conflict(new { success = false, message = error });
        return Ok(new { success = true, data = s });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(Guid id)
    {
        if (!_subjectService.Delete(id)) return NotFound(new { success = false, message = "Subject not found." });
        return Ok(new { success = true, message = "Subject deleted." });
    }
}
