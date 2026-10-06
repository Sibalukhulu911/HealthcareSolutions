using System.ComponentModel.DataAnnotations;
using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.User;

public class UserCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;
}
