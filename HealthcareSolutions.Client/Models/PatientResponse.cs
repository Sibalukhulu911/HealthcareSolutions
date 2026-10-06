using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class PatientResponse
{
    public int PatientId { get; set; }

    [JsonPropertyName("patientFirstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("patientLastName")]
    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}