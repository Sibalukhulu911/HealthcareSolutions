using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class PractitionerCreateRequest
{
    [Required(ErrorMessage = "A user account is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100)]
    [JsonPropertyName("practitionerFirstName")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100)]
    [JsonPropertyName("practitionerLastName")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialty is required.")]
    [StringLength(100)]
    public string Specialty { get; set; } = string.Empty;

    [Required(ErrorMessage = "Office number is required.")]
    [StringLength(20)]
    [JsonPropertyName("officeNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;
}