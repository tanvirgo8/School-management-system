using SchoolManagement.API.Models;

namespace SchoolManagement.API.Data;

/// <summary>
/// In-memory singleton data store.
/// Swap for EF Core DbContext when PostgreSQL is connected.
/// </summary>
public class InMemoryDataStore
{
    private static readonly Lazy<InMemoryDataStore> _instance =
        new(() => new InMemoryDataStore());

    public static InMemoryDataStore Instance => _instance.Value;

    public List<User> Users { get; } = new();
    public List<Class> Classes { get; } = new();
    public List<Subject> Subjects { get; } = new();
    public List<TeacherAssignment> TeacherAssignments { get; } = new();
    public List<Assignment> Assignments { get; } = new();
    public List<Submission> Submissions { get; } = new();

    private InMemoryDataStore()
    {
        SeedData();
    }

    private void SeedData()
    {
        // ─── Classes ────────────────────────────────────────
        var class6 = new Class { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "Class 6", Description = "Grade 6", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) };
        var class7 = new Class { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Class 7", Description = "Grade 7", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) };
        var class10 = new Class { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Class 10", Description = "Grade 10 - Senior Secondary", IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) };
        Classes.AddRange(new[] { class6, class7, class10 });

        // ─── Subjects ───────────────────────────────────────
        var math = new Subject { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), Name = "Mathematics", Code = "MATH101", Description = "Core mathematics", IsActive = true };
        var english = new Subject { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), Name = "English", Code = "ENG101", Description = "English language & literature", IsActive = true };
        var physics = new Subject { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), Name = "Physics", Code = "PHY101", Description = "General physics", IsActive = true };
        var chemistry = new Subject { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), Name = "Chemistry", Code = "CHEM101", Description = "General chemistry", IsActive = true };
        var ict = new Subject { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), Name = "ICT", Code = "ICT101", Description = "Information & Communications Technology", IsActive = true };
        Subjects.AddRange(new[] { math, english, physics, chemistry, ict });

        // ─── Users ──────────────────────────────────────────
        // Admin
        var admin = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
            FullName = "System Administrator",
            Email = "admin@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = UserRole.ADMIN,
            Phone = "+1-555-0100",
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddMonths(-12)
        };

        // Teachers
        var teacher1 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
            FullName = "John Smith",
            Email = "teacher@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
            Role = UserRole.TEACHER,
            Phone = "+1-555-0101",
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddMonths(-10)
        };
        var teacher2 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
            FullName = "Sarah Johnson",
            Email = "sarah.johnson@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
            Role = UserRole.TEACHER,
            Phone = "+1-555-0102",
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddMonths(-8)
        };

        // Students
        var student1 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
            FullName = "Alice Brown",
            Email = "student@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.STUDENT,
            Phone = "+1-555-0201",
            IsActive = true,
            ClassId = class10.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-6)
        };
        var student2 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
            FullName = "Bob Davis",
            Email = "bob.davis@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.STUDENT,
            Phone = "+1-555-0202",
            IsActive = true,
            ClassId = class10.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-6)
        };
        var student3 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000006"),
            FullName = "Carol White",
            Email = "carol.white@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.STUDENT,
            Phone = "+1-555-0203",
            IsActive = true,
            ClassId = class7.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-5)
        };
        var student4 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000007"),
            FullName = "David Martinez",
            Email = "david.martinez@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.STUDENT,
            Phone = "+1-555-0204",
            IsActive = true,
            ClassId = class6.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-5)
        };
        var student5 = new User
        {
            Id = Guid.Parse("30000000-0000-0000-0000-000000000008"),
            FullName = "Eva Garcia",
            Email = "eva.garcia@school.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.STUDENT,
            Phone = "+1-555-0205",
            IsActive = true,
            ClassId = class10.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-4)
        };
        Users.AddRange(new[] { admin, teacher1, teacher2, student1, student2, student3, student4, student5 });

        // ─── Teacher Assignments ─────────────────────────────
        var ta1 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1.Id, ClassId = class10.Id, SubjectId = math.Id };
        var ta2 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1.Id, ClassId = class10.Id, SubjectId = physics.Id };
        var ta3 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1.Id, ClassId = class7.Id, SubjectId = math.Id };
        var ta4 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2.Id, ClassId = class10.Id, SubjectId = english.Id };
        var ta5 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2.Id, ClassId = class6.Id, SubjectId = english.Id };
        var ta6 = new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2.Id, ClassId = class7.Id, SubjectId = chemistry.Id };
        TeacherAssignments.AddRange(new[] { ta1, ta2, ta3, ta4, ta5, ta6 });

        // ─── Assignments ─────────────────────────────────────
        var asgn1 = new Assignment
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
            Title = "Algebra Basics - Quadratic Equations",
            Description = "Solve the following quadratic equations and show all working steps. Problems cover factoring, completing the square, and the quadratic formula.",
            TeacherId = teacher1.Id,
            ClassId = class10.Id,
            SubjectId = math.Id,
            Deadline = DateTime.UtcNow.AddDays(7),
            MaxMarks = 100,
            Status = AssignmentStatus.PUBLISHED,
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        };
        var asgn2 = new Assignment
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
            Title = "Essay Writing - Argumentative Essay",
            Description = "Write a 500-word argumentative essay on the topic: 'Technology has more benefits than drawbacks in modern education.' Support your argument with evidence and examples.",
            TeacherId = teacher2.Id,
            ClassId = class10.Id,
            SubjectId = english.Id,
            Deadline = DateTime.UtcNow.AddDays(5),
            MaxMarks = 50,
            Status = AssignmentStatus.PUBLISHED,
            CreatedAt = DateTime.UtcNow.AddDays(-5)
        };
        var asgn3 = new Assignment
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
            Title = "Newton's Laws of Motion - Problem Set",
            Description = "Complete the problem set on Newton's three laws of motion. Include free-body diagrams where applicable.",
            TeacherId = teacher1.Id,
            ClassId = class10.Id,
            SubjectId = physics.Id,
            Deadline = DateTime.UtcNow.AddDays(-2),
            MaxMarks = 80,
            Status = AssignmentStatus.CLOSED,
            CreatedAt = DateTime.UtcNow.AddDays(-15)
        };
        var asgn4 = new Assignment
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000004"),
            Title = "Fractions and Decimals",
            Description = "Complete exercises on converting fractions to decimals, adding fractions with different denominators, and word problems.",
            TeacherId = teacher1.Id,
            ClassId = class7.Id,
            SubjectId = math.Id,
            Deadline = DateTime.UtcNow.AddDays(10),
            MaxMarks = 60,
            Status = AssignmentStatus.PUBLISHED,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };
        var asgn5 = new Assignment
        {
            Id = Guid.Parse("40000000-0000-0000-0000-000000000005"),
            Title = "Introduction to Chemical Bonding",
            Description = "This assignment is in draft. Content to be finalized.",
            TeacherId = teacher2.Id,
            ClassId = class7.Id,
            SubjectId = chemistry.Id,
            Deadline = DateTime.UtcNow.AddDays(14),
            MaxMarks = 70,
            Status = AssignmentStatus.DRAFT,
            CreatedAt = DateTime.UtcNow
        };
        Assignments.AddRange(new[] { asgn1, asgn2, asgn3, asgn4, asgn5 });

        // ─── Submissions ─────────────────────────────────────
        // student1 submitted asgn1 (Math) — graded
        var sub1 = new Submission
        {
            Id = Guid.NewGuid(),
            AssignmentId = asgn1.Id,
            StudentId = student1.Id,
            Answer = "1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3\n2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3\n3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3",
            SubmittedAt = DateTime.UtcNow.AddDays(-2),
            UpdatedAt = DateTime.UtcNow.AddDays(-2),
            Marks = 88,
            Feedback = "Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.",
            Status = SubmissionStatus.GRADED
        };
        // student2 submitted asgn1 — under review
        var sub2 = new Submission
        {
            Id = Guid.NewGuid(),
            AssignmentId = asgn1.Id,
            StudentId = student2.Id,
            Answer = "1) x = 2 or x = 3\n2) x = -0.5 or x = -3\n3) x = 3 or x = -3\n(Solved using quadratic formula for all)",
            SubmittedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
            Status = SubmissionStatus.UNDER_REVIEW
        };
        // student1 submitted asgn2 (English) — submitted
        var sub3 = new Submission
        {
            Id = Guid.NewGuid(),
            AssignmentId = asgn2.Id,
            StudentId = student1.Id,
            Answer = "Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...",
            SubmittedAt = DateTime.UtcNow.AddHours(-6),
            UpdatedAt = DateTime.UtcNow.AddHours(-6),
            Status = SubmissionStatus.SUBMITTED
        };
        // student1 submitted asgn3 (Physics, closed) — graded
        var sub4 = new Submission
        {
            Id = Guid.NewGuid(),
            AssignmentId = asgn3.Id,
            StudentId = student1.Id,
            Answer = "Newton's First Law: An object at rest stays at rest... [detailed answers]",
            SubmittedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow.AddDays(-5),
            Marks = 72,
            Feedback = "Good understanding of the first two laws. Work on applying the third law in complex systems.",
            Status = SubmissionStatus.GRADED
        };
        Submissions.AddRange(new[] { sub1, sub2, sub3, sub4 });
    }
}
