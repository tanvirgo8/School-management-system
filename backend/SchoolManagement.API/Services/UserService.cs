using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Auth;
using SchoolManagement.API.DTOs.Users;
using SchoolManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.API.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(ApplicationDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<UserDto> GetAll(string? role = null, string? search = null)
    {
        var query = _context.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(role) && Enum.TryParse<UserRole>(role, true, out var roleEnum))
            query = query.Where(u => u.Role == roleEnum);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(u =>
                u.FullName.ToLower().Contains(searchLower) ||
                u.Email.ToLower().Contains(searchLower));
        }

        return query.Select(MapToDto).ToList();
    }

    public UserDto? GetById(Guid id)
    {
        var u = _context.Users.FirstOrDefault(x => x.Id == id);
        return u == null ? null : MapToDto(u);
    }

    public (UserDto? user, string? error) Create(CreateUserRequest req)
    {
        if (_context.Users.Any(u => u.Email.ToLower() == req.Email.ToLower()))
            return (null, "A user with this email already exists.");

        if (!Enum.TryParse<UserRole>(req.Role, true, out var role))
            return (null, "Invalid role. Must be ADMIN, TEACHER, or STUDENT.");

        var user = new User
        {
            FullName = req.FullName,
            Email = req.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Role = role,
            Phone = req.Phone,
            IsActive = req.IsActive,
            ClassId = req.ClassId
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        _logger.LogInformation("User created: {Email} ({Role})", user.Email, user.Role);
        return (MapToDto(user), null);
    }

    public (UserDto? user, string? error) Update(Guid id, UpdateUserRequest req)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return (null, "User not found.");

        if (_context.Users.Any(u => u.Id != id && u.Email.ToLower() == req.Email.ToLower()))
            return (null, "A user with this email already exists.");

        if (!Enum.TryParse<UserRole>(req.Role, true, out var role))
            return (null, "Invalid role.");

        user.FullName = req.FullName;
        user.Email = req.Email;
        user.Role = role;
        user.Phone = req.Phone;
        user.IsActive = req.IsActive;
        user.ClassId = req.ClassId;
        user.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(req.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

        _context.SaveChanges();
        _logger.LogInformation("User updated: {Id}", id);
        return (MapToDto(user), null);
    }

    public bool Delete(Guid id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;
        _context.Users.Remove(user);
        _context.SaveChanges();
        _logger.LogInformation("User deleted: {Id}", id);
        return true;
    }

    private static UserDto MapToDto(User u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Email = u.Email,
        Role = u.Role.ToString(),
        Phone = u.Phone,
        IsActive = u.IsActive,
        CreatedAt = u.CreatedAt,
        ClassId = u.ClassId
    };
}
