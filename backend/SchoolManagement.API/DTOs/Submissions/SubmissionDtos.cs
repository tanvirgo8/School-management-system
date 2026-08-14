namespace SchoolManagement.API.DTOs.Submissions;

public class CreateSubmissionRequest
{
    public Guid AssignmentId { get; set; }
    public string Answer { get; set; } = string.Empty;
    public string? PdfUrl { get; set; }
}

public class UpdateSubmissionRequest
{
    public string Answer { get; set; } = string.Empty;
    public string? PdfUrl { get; set; }
}

public class GradeSubmissionRequest
{
    public int Marks { get; set; }
    public string? Feedback { get; set; }
    public string Status { get; set; } = "GRADED";
}

public class SubmissionDto
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public string AssignmentTitle { get; set; } = string.Empty;
    public int AssignmentMaxMarks { get; set; }
    public DateTime AssignmentDeadline { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string? PdfUrl { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? Marks { get; set; }
    public string? Feedback { get; set; }
    public string Status { get; set; } = string.Empty;
}
