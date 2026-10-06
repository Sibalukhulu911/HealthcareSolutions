namespace HealthcareSolutions.Client.Models;

public class UserResponse
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Role { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}