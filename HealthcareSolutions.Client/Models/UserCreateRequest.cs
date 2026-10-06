using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class UserCreateRequest
{
    [Required(ErrorMessage = "Username is required.")]
    [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string Password { get; set; } = string.Empty;

    public int Role { get; set; }

    public bool IsActive { get; set; } = true;
}