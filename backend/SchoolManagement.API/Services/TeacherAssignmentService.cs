using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.TeacherAssignments;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class TeacherAssignmentService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TeacherAssignmentService> _logger;

    public TeacherAssignmentService(ApplicationDbContext context, ILogger<TeacherAssignmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<TeacherAssignmentDto> GetAll(Guid? teacherId = null)
    {
        var query = _context.TeacherAssignments.ToList();
        if (teacherId.HasValue)
            query = query.Where(ta => ta.TeacherId == teacherId.Value).ToList();
        return query.Select(MapToDto).ToList();
    }

    public TeacherAssignmentDto? GetById(Guid id)
    {
        var ta = _context.TeacherAssignments.FirstOrDefault(x => x.Id == id);
        return ta == null ? null : MapToDto(ta);
    }

    public (TeacherAssignmentDto? result, string? error) Create(CreateTeacherAssignmentRequest req)
    {
        // Validate teacher exists
        var teacher = _context.Users.FirstOrDefault(u => u.Id == req.TeacherId && u.Role == UserRole.TEACHER);
        if (teacher == null) return (null, "Teacher not found.");

        // Validate class exists
        if (!_context.Classes.Any(c => c.Id == req.ClassId))
            return (null, "Class not found.");

        // Validate subject exists
        if (!_context.Subjects.Any(s => s.Id == req.SubjectId))
            return (null, "Subject not found.");

        // Prevent duplicates
        if (_context.TeacherAssignments.Any(ta =>
            ta.TeacherId == req.TeacherId &&
            ta.ClassId == req.ClassId &&
            ta.SubjectId == req.SubjectId))
            return (null, "This teacher-class-subject assignment already exists.");

        var ta = new TeacherAssignment
        {
            TeacherId = req.TeacherId,
            ClassId = req.ClassId,
            SubjectId = req.SubjectId
        };

        _context.TeacherAssignments.Add(ta);
        _context.SaveChanges();
        _logger.LogInformation("Teacher assignment created: Teacher={TeacherId}, Class={ClassId}, Subject={SubjectId}",
            req.TeacherId, req.ClassId, req.SubjectId);

        return (MapToDto(ta), null);
    }

    public bool Delete(Guid id)
    {
        var ta = _context.TeacherAssignments.FirstOrDefault(x => x.Id == id);
        if (ta == null) return false;
        _context.TeacherAssignments.Remove(ta);
        _context.SaveChanges();
        return true;
    }

    private TeacherAssignmentDto MapToDto(TeacherAssignment ta)
    {
        var teacher = _context.Users.FirstOrDefault(u => u.Id == ta.TeacherId);
        var cls = _context.Classes.FirstOrDefault(c => c.Id == ta.ClassId);
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == ta.SubjectId);

        return new TeacherAssignmentDto
        {
            Id = ta.Id,
            TeacherId = ta.TeacherId,
            TeacherName = teacher?.FullName ?? "Unknown",
            TeacherEmail = teacher?.Email ?? "",
            ClassId = ta.ClassId,
            ClassName = cls?.Name ?? "Unknown",
            SubjectId = ta.SubjectId,
            SubjectName = subject?.Name ?? "Unknown",
            SubjectCode = subject?.Code ?? "",
            CreatedAt = ta.CreatedAt
        };
    }
}
