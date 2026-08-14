using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Submissions;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class SubmissionService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(ApplicationDbContext context, ILogger<SubmissionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<SubmissionDto> GetAll(Guid? assignmentId = null, Guid? studentId = null, Guid? teacherId = null)
    {
        var query = _context.Submissions.ToList();

        if (assignmentId.HasValue)
            query = query.Where(s => s.AssignmentId == assignmentId.Value).ToList();

        if (studentId.HasValue)
            query = query.Where(s => s.StudentId == studentId.Value).ToList();

        // Teacher: only see submissions for their assignments
        if (teacherId.HasValue)
        {
            var teacherAssignmentIds = _context.Assignments
                .Where(a => a.TeacherId == teacherId.Value)
                .Select(a => a.Id)
                .ToHashSet();
            query = query.Where(s => teacherAssignmentIds.Contains(s.AssignmentId)).ToList();
        }

        return query.OrderByDescending(s => s.SubmittedAt).Select(MapToDto).ToList();
    }

    public SubmissionDto? GetById(Guid id, Guid? requestingUserId = null, bool isAdmin = false, bool isTeacher = false)
    {
        var submission = _context.Submissions.FirstOrDefault(s => s.Id == id);
        if (submission == null) return null;

        // Authorization
        if (!isAdmin && !isTeacher && requestingUserId.HasValue && submission.StudentId != requestingUserId.Value)
            return null; // Student cannot see another student's submission

        if (isTeacher && !isAdmin && requestingUserId.HasValue)
        {
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == submission.AssignmentId);
            if (assignment == null || assignment.TeacherId != requestingUserId.Value)
                return null; // Teacher can only see submissions for their assignments
        }

        return MapToDto(submission);
    }

    public (SubmissionDto? result, string? error) Create(CreateSubmissionRequest req, Guid studentId)
    {
        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == req.AssignmentId);
        if (assignment == null) return (null, "Assignment not found.");

        if (assignment.Status != AssignmentStatus.PUBLISHED)
            return (null, "This assignment is not available for submission.");

        // Check student is in the right class
        var student = _context.Users.FirstOrDefault(u => u.Id == studentId);
        if (student == null || student.ClassId != assignment.ClassId)
            return (null, "This assignment is not assigned to your class.");

        // Check deadline
        if (DateTime.UtcNow > assignment.Deadline)
            return (null, "The submission deadline has passed.");

        // Prevent duplicate submissions
        if (_context.Submissions.Any(s => s.AssignmentId == req.AssignmentId && s.StudentId == studentId))
            return (null, "You have already submitted this assignment.");

        var submission = new Submission
        {
            AssignmentId = req.AssignmentId,
            StudentId = studentId,
            Answer = req.Answer,
            PdfUrl = req.PdfUrl,
            Status = SubmissionStatus.SUBMITTED
        };

        _context.Submissions.Add(submission);
        _context.SaveChanges();
        _logger.LogInformation("Submission created: Assignment={AssignmentId}, Student={StudentId}", req.AssignmentId, studentId);
        return (MapToDto(submission), null);
    }

    public (SubmissionDto? result, string? error) Update(Guid id, UpdateSubmissionRequest req, Guid studentId)
    {
        var submission = _context.Submissions.FirstOrDefault(s => s.Id == id);
        if (submission == null) return (null, "Submission not found.");

        if (submission.StudentId != studentId)
            return (null, "You are not authorized to update this submission.");

        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == submission.AssignmentId);
        if (assignment == null) return (null, "Assignment not found.");

        if (DateTime.UtcNow > assignment.Deadline)
            return (null, "The submission deadline has passed. You can no longer update your submission.");

        submission.Answer = req.Answer;
        submission.PdfUrl = req.PdfUrl;
        submission.UpdatedAt = DateTime.UtcNow;
        submission.Status = SubmissionStatus.SUBMITTED;

        _context.SaveChanges();
        _logger.LogInformation("Submission updated: {Id}", id);
        return (MapToDto(submission), null);
    }

    public (SubmissionDto? result, string? error) Grade(Guid id, GradeSubmissionRequest req, Guid teacherId, bool isAdmin = false)
    {
        var submission = _context.Submissions.FirstOrDefault(s => s.Id == id);
        if (submission == null) return (null, "Submission not found.");

        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == submission.AssignmentId);
        if (assignment == null) return (null, "Assignment not found.");

        if (!isAdmin && assignment.TeacherId != teacherId)
            return (null, "You are not authorized to grade this submission.");

        if (req.Marks < 0)
            return (null, "Marks cannot be negative.");

        if (req.Marks > assignment.MaxMarks)
            return (null, $"Marks cannot exceed maximum marks ({assignment.MaxMarks}).");

        if (!Enum.TryParse<SubmissionStatus>(req.Status, true, out var status))
            status = SubmissionStatus.GRADED;

        submission.Marks = req.Marks;
        submission.Feedback = req.Feedback;
        submission.Status = status;
        submission.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        _logger.LogInformation("Submission graded: {Id}, Marks={Marks}", id, req.Marks);
        return (MapToDto(submission), null);
    }

    private SubmissionDto MapToDto(Submission s)
    {
        var assignment = _context.Assignments.FirstOrDefault(a => a.Id == s.AssignmentId);
        var student = _context.Users.FirstOrDefault(u => u.Id == s.StudentId);

        return new SubmissionDto
        {
            Id = s.Id,
            AssignmentId = s.AssignmentId,
            AssignmentTitle = assignment?.Title ?? "Unknown",
            AssignmentMaxMarks = assignment?.MaxMarks ?? 0,
            AssignmentDeadline = assignment?.Deadline ?? DateTime.MinValue,
            StudentId = s.StudentId,
            StudentName = student?.FullName ?? "Unknown",
            StudentEmail = student?.Email ?? "",
            Answer = s.Answer,
            PdfUrl = s.PdfUrl,
            SubmittedAt = s.SubmittedAt,
            UpdatedAt = s.UpdatedAt,
            Marks = s.Marks,
            Feedback = s.Feedback,
            Status = s.Status.ToString()
        };
    }
}
