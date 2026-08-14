using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Auth;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApplicationDbContext context, TokenService tokenService, ILogger<AuthService> logger)
    {
        _context = context;
        _tokenService = tokenService;
        _logger = logger;
    }

    public LoginResponse Login(LoginRequest request)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = _context.Users.FirstOrDefault(u =>
            u.Email.ToLower() == request.Email.ToLower());

        if (user == null)
        {
            _logger.LogWarning("Login failed - user not found: {Email}", request.Email);
            return new LoginResponse { Success = false, Message = "Invalid email or password." };
        }

        if (!user.IsActive)
        {
            _logger.LogWarning("Login failed - inactive account: {Email}", request.Email);
            return new LoginResponse { Success = false, Message = "Your account has been deactivated. Please contact the administrator." };
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Login failed - invalid password: {Email}", request.Email);
            return new LoginResponse { Success = false, Message = "Invalid email or password." };
        }

        var token = _tokenService.GenerateToken(user);
        _logger.LogInformation("Login successful: {Email} ({Role})", user.Email, user.Role);

        return new LoginResponse
        {
            Success = true,
            Token = token,
            Message = "Login successful.",
            User = MapToDto(user)
        };
    }

    public UserDto? GetUserById(Guid userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        return user == null ? null : MapToDto(user);
    }

    private static UserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        Role = user.Role.ToString(),
        Phone = user.Phone,
        IsActive = user.IsActive,
        CreatedAt = user.CreatedAt,
        ClassId = user.ClassId
    };
}
