using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class PatientUpdateRequest
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100)]
    [JsonPropertyName("patientFirstName")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100)]
    [JsonPropertyName("patientLastName")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of birth is required.")]
    [JsonPropertyName("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(20)]
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }
}