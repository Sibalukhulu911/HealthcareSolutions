using System.ComponentModel.DataAnnotations;
using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.User;

public class UserUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    public bool IsActive { get; set; }
}
