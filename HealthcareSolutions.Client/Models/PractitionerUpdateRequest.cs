using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class PractitionerUpdateRequest
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100)]
    [JsonPropertyName("practitionerFirstName")]
    public string PractitionerFirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100)]
    [JsonPropertyName("practitionerLastName")]
    public string PractitionerLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialty is required.")]
    [StringLength(50)]
    public string Specialty { get; set; } = string.Empty;

    [Required(ErrorMessage = "Office number is required.")]
    [StringLength(30)]
    [JsonPropertyName("officeNumber")]
    public string OfficeNumber { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
}