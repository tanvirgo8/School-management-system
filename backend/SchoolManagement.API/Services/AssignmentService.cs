using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Assignments;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class AssignmentService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AssignmentService> _logger;

    public AssignmentService(ApplicationDbContext context, ILogger<AssignmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<AssignmentDto> GetAll(Guid? teacherId = null, Guid? classId = null, string? status = null)
    {
        var query = _context.Assignments.ToList();

        if (teacherId.HasValue)
            query = query.Where(a => a.TeacherId == teacherId.Value).ToList();

        if (classId.HasValue)
            query = query.Where(a => a.ClassId == classId.Value).ToList();

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<AssignmentStatus>(status, true, out var statusEnum))
            query = query.Where(a => a.Status == statusEnum).ToList();

        return query.OrderByDescending(a => a.CreatedAt).Select(MapToDto).ToList();
    }

    /// <summary>Get published assignments for a specific class (used by students)</summary>
    public IEnumerable<AssignmentDto> GetForStudent(Guid classId)
    {
        var assignments = _context.Assignments
            .Where(a => a.ClassId == classId && a.Status == AssignmentStatus.PUBLISHED)
            .OrderByDescending(a => a.Deadline)
            .ToList();
        return assignments.Select(MapToDto).ToList();
    }

    public AssignmentDto? GetById(Guid id)
    {
        var a = _context.Assignments.FirstOrDefault(x => x.Id == id);
        return a == null ? null : MapToDto(a);
    }

    public (AssignmentDto? result, string? error) Create(CreateAssignmentRequest req, Guid teacherId)
    {
        // Verify teacher is assigned to this class/subject
        var hasAccess = _context.TeacherAssignments.Any(ta =>
            ta.TeacherId == teacherId &&
            ta.ClassId == req.ClassId &&
            ta.SubjectId == req.SubjectId);

        if (!hasAccess)
            return (null, "You are not authorized to create assignments for this class and subject combination.");

        if (!Enum.TryParse<AssignmentStatus>(req.Status, true, out var status))
            status = AssignmentStatus.DRAFT;

        if (req.MaxMarks <= 0)
            return (null, "Maximum marks must be greater than 0.");

        if (req.Deadline <= DateTime.UtcNow)
            return (null, "Deadline must be in the future.");

        var assignment = new Assignment
        {
            Title = req.Title,
            Description = req.Description,
            PdfUrl = req.PdfUrl,
            TeacherId = teacherId,
            ClassId = req.ClassId,
            SubjectId = req.SubjectId,
            Deadline = req.Deadline,
            MaxMarks = req.MaxMarks,
            Status = status
        };

        _context.Assignments.Add(assignment);
        _context.SaveChanges();
        _logger.LogInformation("Assignment created: {Title} by Teacher={TeacherId}", assignment.Title, teacherId);
        return (MapToDto(assignment), null);
    }

    public (AssignmentDto? result, string? error) Update(Guid id, UpdateAssignmentRequest req, Guid teacherId, bool isAdmin = false)
    {
        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == id);
        if (assignment == null) return (null, "Assignment not found.");

        if (!isAdmin && assignment.TeacherId != teacherId)
            return (null, "You are not authorized to modify this assignment.");

        if (!Enum.TryParse<AssignmentStatus>(req.Status, true, out var status))
            status = assignment.Status;

        if (req.MaxMarks <= 0)
            return (null, "Maximum marks must be greater than 0.");

        assignment.Title = req.Title;
        assignment.Description = req.Description;
        assignment.PdfUrl = req.PdfUrl;
        assignment.ClassId = req.ClassId;
        assignment.SubjectId = req.SubjectId;
        assignment.Deadline = req.Deadline;
        assignment.MaxMarks = req.MaxMarks;
        assignment.Status = status;
        assignment.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        _logger.LogInformation("Assignment updated: {Id}", id);
        return (MapToDto(assignment), null);
    }

    public (AssignmentDto? result, string? error) Publish(Guid id, Guid teacherId, bool isAdmin = false)
    {
        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == id);
        if (assignment == null) return (null, "Assignment not found.");

        if (!isAdmin && assignment.TeacherId != teacherId)
            return (null, "You are not authorized to publish this assignment.");

        assignment.Status = AssignmentStatus.PUBLISHED;
        assignment.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        _logger.LogInformation("Assignment published: {Id}", id);
        return (MapToDto(assignment), null);
    }

    public (bool success, string? error) Delete(Guid id, Guid teacherId, bool isAdmin = false)
    {
        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == id);
        if (assignment == null) return (false, "Assignment not found.");

        if (!isAdmin && assignment.TeacherId != teacherId)
            return (false, "You are not authorized to delete this assignment.");

        _context.Assignments.Remove(assignment);
        _context.SaveChanges();
        _logger.LogInformation("Assignment deleted: {Id}", id);
        return (true, null);
    }

    private AssignmentDto MapToDto(Assignment a)
    {
        var teacher = _context.Users.FirstOrDefault(u => u.Id == a.TeacherId);
        var cls = _context.Classes.FirstOrDefault(c => c.Id == a.ClassId);
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == a.SubjectId);
        var submissionCount = _context.Submissions.Count(s => s.AssignmentId == a.Id);

        return new AssignmentDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            PdfUrl = a.PdfUrl,
            TeacherId = a.TeacherId,
            TeacherName = teacher?.FullName ?? "Unknown",
            ClassId = a.ClassId,
            ClassName = cls?.Name ?? "Unknown",
            SubjectId = a.SubjectId,
            SubjectName = subject?.Name ?? "Unknown",
            SubjectCode = subject?.Code ?? "",
            Deadline = a.Deadline,
            MaxMarks = a.MaxMarks,
            Status = a.Status.ToString(),
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            SubmissionCount = submissionCount
        };
    }
}
