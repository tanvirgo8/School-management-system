namespace SchoolManagement.API.DTOs.Classes;

public class CreateClassRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TuitionFee { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateClassRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TuitionFee { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TuitionFee { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int StudentCount { get; set; }
}
