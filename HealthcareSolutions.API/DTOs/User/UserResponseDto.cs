using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.User;

public class UserResponseDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
