using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Classes;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class ClassService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ClassService> _logger;

    public ClassService(ApplicationDbContext context, ILogger<ClassService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<ClassDto> GetAll()
    {
        return _context.Classes.AsEnumerable().Select(c => MapToDto(c)).ToList();
    }

    public ClassDto? GetById(Guid id)
    {
        var c = _context.Classes.FirstOrDefault(x => x.Id == id);
        return c == null ? null : MapToDto(c);
    }

    public (ClassDto? cls, string? error) Create(CreateClassRequest req)
    {
        if (_context.Classes.Any(c => c.Name.ToLower() == req.Name.ToLower()))
            return (null, "A class with this name already exists.");

        var cls = new Class
        {
            Name = req.Name,
            Description = req.Description,
            TuitionFee = req.TuitionFee,
            IsActive = req.IsActive
        };

        _context.Classes.Add(cls);
        _context.SaveChanges();
        _logger.LogInformation("Class created: {Name}", cls.Name);
        return (MapToDto(cls), null);
    }

    public (ClassDto? cls, string? error) Update(Guid id, UpdateClassRequest req)
    {
        var cls = _context.Classes.FirstOrDefault(c => c.Id == id);
        if (cls == null) return (null, "Class not found.");

        if (_context.Classes.Any(c => c.Id != id && c.Name.ToLower() == req.Name.ToLower()))
            return (null, "A class with this name already exists.");

        cls.Name = req.Name;
        cls.Description = req.Description;
        cls.TuitionFee = req.TuitionFee;
        cls.IsActive = req.IsActive;

        _context.SaveChanges();
        _logger.LogInformation("Class updated: {Id}", id);
        return (MapToDto(cls), null);
    }

    public bool Delete(Guid id)
    {
        var cls = _context.Classes.FirstOrDefault(c => c.Id == id);
        if (cls == null) return false;
        _context.Classes.Remove(cls);
        _context.SaveChanges();
        return true;
    }

    private ClassDto MapToDto(Class c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description,
        TuitionFee = c.TuitionFee,
        IsActive = c.IsActive,
        CreatedAt = c.CreatedAt,
        StudentCount = _context.Users.Count(u => u.ClassId == c.Id && u.Role == Models.UserRole.STUDENT)
    };
}
