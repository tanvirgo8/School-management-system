using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Subjects;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class SubjectService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SubjectService> _logger;

    public SubjectService(ApplicationDbContext context, ILogger<SubjectService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<SubjectDto> GetAll(Guid? classId = null)
    {
        var query = _context.Subjects.AsQueryable();
        if (classId.HasValue)
        {
            query = query.Where(s => s.ClassId == classId.Value);
        }
        return query.Select(s => new SubjectDto
        {
            Id = s.Id,
            Name = s.Name,
            Code = s.Code,
            Description = s.Description,
            ClassId = s.ClassId,
            ClassName = _context.Classes.Where(c => c.Id == s.ClassId).Select(c => c.Name).FirstOrDefault() ?? string.Empty,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt
        }).ToList();
    }

    public SubjectDto? GetById(Guid id)
    {
        var s = _context.Subjects.FirstOrDefault(x => x.Id == id);
        return s == null ? null : MapToDto(s);
    }

    public (SubjectDto? subject, string? error) Create(CreateSubjectRequest req)
    {
        if (_context.Subjects.Any(s => s.Code.ToLower() == req.Code.ToLower() && s.ClassId == req.ClassId))
            return (null, "A subject with this code already exists in this class.");

        var subject = new Subject
        {
            Name = req.Name,
            Code = req.Code,
            Description = req.Description,
            ClassId = req.ClassId,
            IsActive = req.IsActive
        };

        _context.Subjects.Add(subject);
        _context.SaveChanges();
        _logger.LogInformation("Subject created: {Name} ({Code})", subject.Name, subject.Code);
        return (MapToDto(subject), null);
    }

    public (SubjectDto? subject, string? error) Update(Guid id, UpdateSubjectRequest req)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null) return (null, "Subject not found.");

        if (_context.Subjects.Any(s => s.Id != id && s.Code.ToLower() == req.Code.ToLower() && s.ClassId == req.ClassId))
            return (null, "A subject with this code already exists in this class.");

        subject.Name = req.Name;
        subject.Code = req.Code;
        subject.Description = req.Description;
        subject.ClassId = req.ClassId;
        subject.IsActive = req.IsActive;

        _context.SaveChanges();
        _logger.LogInformation("Subject updated: {Id}", id);
        return (MapToDto(subject), null);
    }

    public bool Delete(Guid id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null) return false;
        _context.Subjects.Remove(subject);
        _context.SaveChanges();
        return true;
    }

    private SubjectDto MapToDto(Subject s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Code = s.Code,
        Description = s.Description,
        ClassId = s.ClassId,
        ClassName = _context.Classes.Where(c => c.Id == s.ClassId).Select(c => c.Name).FirstOrDefault() ?? string.Empty,
        IsActive = s.IsActive,
        CreatedAt = s.CreatedAt
    };
}
