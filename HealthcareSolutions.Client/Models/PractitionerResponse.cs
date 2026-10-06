using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class PractitionerResponse
{
    public int PractitionerId { get; set; }

    public int UserId { get; set; }

    [JsonPropertyName("practitionerFirstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("practitionerLastName")]
    public string LastName { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;

    [JsonPropertyName("officeNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
}