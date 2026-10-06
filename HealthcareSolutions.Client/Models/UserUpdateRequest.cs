using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class UserUpdateRequest
{
    [Required(ErrorMessage = "Username is required.")]
    [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
    public string Username { get; set; } = string.Empty;

    public int Role { get; set; }

    public bool IsActive { get; set; }
}