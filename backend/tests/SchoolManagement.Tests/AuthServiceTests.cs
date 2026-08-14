using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SchoolManagement.API.Data;
using SchoolManagement.API.DTOs.Auth;
using SchoolManagement.API.Models;
using SchoolManagement.API.Services;
using Xunit;

namespace SchoolManagement.Tests;

public class AuthServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly Mock<ILogger<TokenService>> _tokenLoggerMock;
    private readonly string _jwtSecret = "SchoolManagementSuperSecretKey2024!XyZ123";

    public AuthServiceTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        // Setup mock configurations
        _configMock = new Mock<IConfiguration>();
        _configMock.Setup(c => c["Jwt:Secret"]).Returns(_jwtSecret);
        _configMock.Setup(c => c["Jwt:Issuer"]).Returns("SchoolManagement");
        _configMock.Setup(c => c["Jwt:Audience"]).Returns("SchoolManagement");

        _tokenLoggerMock = new Mock<ILogger<TokenService>>();
        _tokenService = new TokenService(_configMock.Object, _tokenLoggerMock.Object);

        _loggerMock = new Mock<ILogger<AuthService>>();
    }

    private User CreateTestUser(string email, string password, UserRole role, bool isActive = true)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FullName = "Test User",
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role,
            Phone = "1234567890",
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    [Fact]
    public void Login_WithValidCredentials_ReturnsSuccessfulLoginResponse()
    {
        // Arrange
        var email = "admin@test.com";
        var password = "Admin@Password123";
        var user = CreateTestUser(email, password, UserRole.ADMIN);
        _context.Users.Add(user);
        _context.SaveChanges();

        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);
        var request = new LoginRequest { Email = email, Password = password };

        // Act
        var response = authService.Login(request);

        // Assert
        Assert.True(response.Success);
        Assert.NotEmpty(response.Token);
        Assert.Equal("Login successful.", response.Message);
        Assert.NotNull(response.User);
        Assert.Equal(user.Email, response.User.Email);
        Assert.Equal("ADMIN", response.User.Role);
    }

    [Fact]
    public void Login_WithInvalidPassword_ReturnsFailureResponse()
    {
        // Arrange
        var email = "teacher@test.com";
        var password = "CorrectPassword123";
        var user = CreateTestUser(email, password, UserRole.TEACHER);
        _context.Users.Add(user);
        _context.SaveChanges();

        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);
        var request = new LoginRequest { Email = email, Password = "WrongPassword" };

        // Act
        var response = authService.Login(request);

        // Assert
        Assert.False(response.Success);
        Assert.Empty(response.Token);
        Assert.Equal("Invalid email or password.", response.Message);
    }

    [Fact]
    public void Login_WithNonExistentUser_ReturnsFailureResponse()
    {
        // Arrange
        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);
        var request = new LoginRequest { Email = "nonexistent@test.com", Password = "somePassword" };

        // Act
        var response = authService.Login(request);

        // Assert
        Assert.False(response.Success);
        Assert.Empty(response.Token);
        Assert.Equal("Invalid email or password.", response.Message);
    }

    [Fact]
    public void Login_WithInactiveUser_ReturnsDeactivatedAccountResponse()
    {
        // Arrange
        var email = "student@test.com";
        var password = "StudentPassword123";
        var user = CreateTestUser(email, password, UserRole.STUDENT, isActive: false);
        _context.Users.Add(user);
        _context.SaveChanges();

        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);
        var request = new LoginRequest { Email = email, Password = password };

        // Act
        var response = authService.Login(request);

        // Assert
        Assert.False(response.Success);
        Assert.Empty(response.Token);
        Assert.Contains("deactivated", response.Message);
    }

    [Fact]
    public void GetUserById_WithValidId_ReturnsUserDto()
    {
        // Arrange
        var user = CreateTestUser("user@test.com", "Password@123", UserRole.STUDENT);
        _context.Users.Add(user);
        _context.SaveChanges();

        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);

        // Act
        var result = authService.GetUserById(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
    }

    [Fact]
    public void GetUserById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var authService = new AuthService(_context, _tokenService, _loggerMock.Object);

        // Act
        var result = authService.GetUserById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
