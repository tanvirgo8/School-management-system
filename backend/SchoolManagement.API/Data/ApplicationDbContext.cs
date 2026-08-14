using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<TeacherAssignment> TeacherAssignments { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<Submission> Submissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<User>()
            .HasOne<Class>()
            .WithMany()
            .HasForeignKey(u => u.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subject>()
            .HasOne<Class>()
            .WithMany()
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeacherAssignment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(ta => ta.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeacherAssignment>()
            .HasOne<Class>()
            .WithMany()
            .HasForeignKey(ta => ta.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TeacherAssignment>()
            .HasOne<Subject>()
            .WithMany()
            .HasForeignKey(ta => ta.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Assignment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Assignment>()
            .HasOne<Class>()
            .WithMany()
            .HasForeignKey(a => a.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Assignment>()
            .HasOne<Subject>()
            .WithMany()
            .HasForeignKey(a => a.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne<Assignment>()
            .WithMany()
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed Data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Classes
        var class6Id = Guid.Parse("10000000-0000-0000-0000-000000000001");
        var class7Id = Guid.Parse("10000000-0000-0000-0000-000000000002");
        var class10Id = Guid.Parse("10000000-0000-0000-0000-000000000003");

        modelBuilder.Entity<Class>().HasData(
            new Class { Id = class6Id, Name = "Class 6", Description = "Grade 6", TuitionFee = 1500, IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) },
            new Class { Id = class7Id, Name = "Class 7", Description = "Grade 7", TuitionFee = 1800, IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) },
            new Class { Id = class10Id, Name = "Class 10", Description = "Grade 10 - Senior Secondary", TuitionFee = 2500, IsActive = true, CreatedAt = DateTime.UtcNow.AddMonths(-6) }
        );

        // Subjects (class-wise duplicate mapped)
        var math10Id = Guid.Parse("20000000-0000-0000-0000-000000000001");
        var math7Id = Guid.Parse("20000000-0000-0000-0000-000000000006");
        var english10Id = Guid.Parse("20000000-0000-0000-0000-000000000002");
        var english6Id = Guid.Parse("20000000-0000-0000-0000-000000000007");
        var physicsId = Guid.Parse("20000000-0000-0000-0000-000000000003");
        var chemistryId = Guid.Parse("20000000-0000-0000-0000-000000000004");
        var ictId = Guid.Parse("20000000-0000-0000-0000-000000000005");

        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = math10Id, Name = "Mathematics", Code = "MATH101", Description = "Core mathematics", ClassId = class10Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = math7Id, Name = "Mathematics", Code = "MATH101", Description = "Core mathematics", ClassId = class7Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = english10Id, Name = "English", Code = "ENG101", Description = "English language & literature", ClassId = class10Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = english6Id, Name = "English", Code = "ENG101", Description = "English language & literature", ClassId = class6Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = physicsId, Name = "Physics", Code = "PHY101", Description = "General physics", ClassId = class10Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = chemistryId, Name = "Chemistry", Code = "CHEM101", Description = "General chemistry", ClassId = class7Id, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Subject { Id = ictId, Name = "ICT", Code = "ICT101", Description = "Information & Communications Technology", ClassId = class10Id, IsActive = true, CreatedAt = DateTime.UtcNow }
        );

        // Users (hashing passwords with BCrypt)
        var adminId = Guid.Parse("30000000-0000-0000-0000-000000000001");
        var teacher1Id = Guid.Parse("30000000-0000-0000-0000-000000000002");
        var teacher2Id = Guid.Parse("30000000-0000-0000-0000-000000000003");
        var student1Id = Guid.Parse("30000000-0000-0000-0000-000000000004");
        var student2Id = Guid.Parse("30000000-0000-0000-0000-000000000005");
        var student3Id = Guid.Parse("30000000-0000-0000-0000-000000000006");
        var student4Id = Guid.Parse("30000000-0000-0000-0000-000000000007");
        var student5Id = Guid.Parse("30000000-0000-0000-0000-000000000008");
        var teacher3Id = Guid.Parse("30000000-0000-0000-0000-000000000009");
        var student6Id = Guid.Parse("30000000-0000-0000-0000-000000000010");
        var student7Id = Guid.Parse("30000000-0000-0000-0000-000000000011");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                FullName = "System Administrator",
                Email = "admin@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.ADMIN,
                Phone = "+1-555-0100",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-12),
                UpdatedAt = DateTime.UtcNow.AddMonths(-12)
            },
            new User
            {
                Id = teacher1Id,
                FullName = "John Smith",
                Email = "teacher@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
                Role = UserRole.TEACHER,
                Phone = "+1-555-0101",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-10),
                UpdatedAt = DateTime.UtcNow.AddMonths(-10)
            },
            new User
            {
                Id = teacher2Id,
                FullName = "Sarah Johnson",
                Email = "sarah.johnson@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
                Role = UserRole.TEACHER,
                Phone = "+1-555-0102",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-8),
                UpdatedAt = DateTime.UtcNow.AddMonths(-8)
            },
            new User
            {
                Id = student1Id,
                FullName = "Alice Brown",
                Email = "student@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0201",
                IsActive = true,
                ClassId = class10Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
                UpdatedAt = DateTime.UtcNow.AddMonths(-6)
            },
            new User
            {
                Id = student2Id,
                FullName = "Bob Davis",
                Email = "bob.davis@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0202",
                IsActive = true,
                ClassId = class10Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
                UpdatedAt = DateTime.UtcNow.AddMonths(-6)
            },
            new User
            {
                Id = student3Id,
                FullName = "Carol White",
                Email = "carol.white@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0203",
                IsActive = true,
                ClassId = class7Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-5),
                UpdatedAt = DateTime.UtcNow.AddMonths(-5)
            },
            new User
            {
                Id = student4Id,
                FullName = "David Martinez",
                Email = "david.martinez@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0204",
                IsActive = true,
                ClassId = class6Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-5),
                UpdatedAt = DateTime.UtcNow.AddMonths(-5)
            },
            new User
            {
                Id = student5Id,
                FullName = "Eva Garcia",
                Email = "eva.garcia@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0205",
                IsActive = true,
                ClassId = class10Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-4),
                UpdatedAt = DateTime.UtcNow.AddMonths(-4)
            },
            new User
            {
                Id = teacher3Id,
                FullName = "Michael Brown",
                Email = "michael.brown@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
                Role = UserRole.TEACHER,
                Phone = "+1-555-0103",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
                UpdatedAt = DateTime.UtcNow.AddMonths(-3)
            },
            new User
            {
                Id = student6Id,
                FullName = "Sophia Wilson",
                Email = "sophia.wilson@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0206",
                IsActive = true,
                ClassId = class10Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
                UpdatedAt = DateTime.UtcNow.AddMonths(-3)
            },
            new User
            {
                Id = student7Id,
                FullName = "James Taylor",
                Email = "james.taylor@school.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.STUDENT,
                Phone = "+1-555-0207",
                IsActive = true,
                ClassId = class7Id,
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
                UpdatedAt = DateTime.UtcNow.AddMonths(-3)
            }
        );

        // Teacher Assignments
        modelBuilder.Entity<TeacherAssignment>().HasData(
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1Id, ClassId = class10Id, SubjectId = math10Id, CreatedAt = DateTime.UtcNow },
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1Id, ClassId = class10Id, SubjectId = physicsId, CreatedAt = DateTime.UtcNow },
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher1Id, ClassId = class7Id, SubjectId = math7Id, CreatedAt = DateTime.UtcNow },
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2Id, ClassId = class10Id, SubjectId = english10Id, CreatedAt = DateTime.UtcNow },
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2Id, ClassId = class6Id, SubjectId = english6Id, CreatedAt = DateTime.UtcNow },
            new TeacherAssignment { Id = Guid.NewGuid(), TeacherId = teacher2Id, ClassId = class7Id, SubjectId = chemistryId, CreatedAt = DateTime.UtcNow }
        );

        // Assignments
        var asgn1Id = Guid.Parse("40000000-0000-0000-0000-000000000001");
        var asgn2Id = Guid.Parse("40000000-0000-0000-0000-000000000002");
        var asgn3Id = Guid.Parse("40000000-0000-0000-0000-000000000003");
        var asgn4Id = Guid.Parse("40000000-0000-0000-0000-000000000004");
        var asgn5Id = Guid.Parse("40000000-0000-0000-0000-000000000005");

        modelBuilder.Entity<Assignment>().HasData(
            new Assignment
            {
                Id = asgn1Id,
                Title = "Algebra Basics - Quadratic Equations",
                Description = "Solve the following quadratic equations and show all working steps. Problems cover factoring, completing the square, and the quadratic formula.",
                TeacherId = teacher1Id,
                ClassId = class10Id,
                SubjectId = math10Id,
                Deadline = DateTime.UtcNow.AddDays(7),
                MaxMarks = 100,
                Status = AssignmentStatus.PUBLISHED,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Assignment
            {
                Id = asgn2Id,
                Title = "Essay Writing - Argumentative Essay",
                Description = "Write a 500-word argumentative essay on the topic: 'Technology has more benefits than drawbacks in modern education.' Support your argument with evidence and examples.",
                TeacherId = teacher2Id,
                ClassId = class10Id,
                SubjectId = english10Id,
                Deadline = DateTime.UtcNow.AddDays(5),
                MaxMarks = 50,
                Status = AssignmentStatus.PUBLISHED,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Assignment
            {
                Id = asgn3Id,
                Title = "Newton's Laws of Motion - Problem Set",
                Description = "Complete the problem set on Newton's three laws of motion. Include free-body diagrams where applicable.",
                TeacherId = teacher1Id,
                ClassId = class10Id,
                SubjectId = physicsId,
                Deadline = DateTime.UtcNow.AddDays(-2),
                MaxMarks = 80,
                Status = AssignmentStatus.CLOSED,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Assignment
            {
                Id = asgn4Id,
                Title = "Fractions and Decimals",
                Description = "Complete exercises on converting fractions to decimals, adding fractions with different denominators, and word problems.",
                TeacherId = teacher1Id,
                ClassId = class7Id,
                SubjectId = math7Id,
                Deadline = DateTime.UtcNow.AddDays(10),
                MaxMarks = 60,
                Status = AssignmentStatus.PUBLISHED,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Assignment
            {
                Id = asgn5Id,
                Title = "Introduction to Chemical Bonding",
                Description = "This assignment is in draft. Content to be finalized.",
                TeacherId = teacher2Id,
                ClassId = class7Id,
                SubjectId = chemistryId,
                Deadline = DateTime.UtcNow.AddDays(14),
                MaxMarks = 70,
                Status = AssignmentStatus.DRAFT,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Submissions
        modelBuilder.Entity<Submission>().HasData(
            new Submission
            {
                Id = Guid.NewGuid(),
                AssignmentId = asgn1Id,
                StudentId = student1Id,
                Answer = "1) x² - 5x + 6 = 0 → (x-2)(x-3) = 0 → x = 2 or x = 3\n2) 2x² + 7x + 3 = 0 → x = (-7 ± √(49-24))/4 → x = -1/2 or x = -3\n3) x² - 9 = 0 → (x-3)(x+3) = 0 → x = ±3",
                SubmittedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2),
                Marks = 88,
                Feedback = "Excellent work! All solutions are correct. Your use of factoring in problem 1 and 3 is efficient. In problem 2, consider showing the discriminant calculation more explicitly.",
                Status = SubmissionStatus.GRADED
            },
            new Submission
            {
                Id = Guid.NewGuid(),
                AssignmentId = asgn1Id,
                StudentId = student2Id,
                Answer = "1) x = 2 or x = 3\n2) x = -0.5 or x = -3\n3) x = 3 or x = -3\n(Solved using quadratic formula for all)",
                SubmittedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1),
                Status = SubmissionStatus.UNDER_REVIEW
            },
            new Submission
            {
                Id = Guid.NewGuid(),
                AssignmentId = asgn2Id,
                StudentId = student1Id,
                Answer = "Technology has revolutionized education in countless ways. From interactive learning platforms to instant access to global resources, the benefits far outweigh the challenges. While critics point to screen time concerns and digital distractions, a structured approach to technology integration enables personalized learning, enhances collaboration, and prepares students for a digital workforce...",
                SubmittedAt = DateTime.UtcNow.AddHours(-6),
                UpdatedAt = DateTime.UtcNow.AddHours(-6),
                Status = SubmissionStatus.SUBMITTED
            },
            new Submission
            {
                Id = Guid.NewGuid(),
                AssignmentId = asgn3Id,
                StudentId = student1Id,
                Answer = "Newton's First Law: An object at rest stays at rest... [detailed answers]",
                SubmittedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
                Marks = 72,
                Feedback = "Good understanding of the first two laws. Work on applying the third law in complex systems.",
                Status = SubmissionStatus.GRADED
            }
        );
    }
}
