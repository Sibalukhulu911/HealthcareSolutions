namespace HealthcareSolutions.Client.Models;

public class UserLookupResponse
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Role { get; set; }

    public bool IsActive { get; set; }
}