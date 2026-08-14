using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs.Classes;
using SchoolManagement.API.Services;

namespace SchoolManagement.API.Controllers;

[ApiController]
[Route("api/classes")]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly ClassService _classService;
    public ClassesController(ClassService classService) => _classService = classService;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { success = true, data = _classService.GetAll() });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var cls = _classService.GetById(id);
        if (cls == null) return NotFound(new { success = false, message = "Class not found." });
        return Ok(new { success = true, data = cls });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create([FromBody] CreateClassRequest req)
    {
        var (cls, error) = _classService.Create(req);
        if (error != null) return Conflict(new { success = false, message = error });
        return Created($"/api/classes/{cls!.Id}", new { success = true, data = cls });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Update(Guid id, [FromBody] UpdateClassRequest req)
    {
        var (cls, error) = _classService.Update(id, req);
        if (error == "Class not found.") return NotFound(new { success = false, message = error });
        if (error != null) return Conflict(new { success = false, message = error });
        return Ok(new { success = true, data = cls });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(Guid id)
    {
        if (!_classService.Delete(id)) return NotFound(new { success = false, message = "Class not found." });
        return Ok(new { success = true, message = "Class deleted." });
    }
}
