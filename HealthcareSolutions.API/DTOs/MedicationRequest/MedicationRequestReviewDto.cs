using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.MedicationRequest;

public class MedicationRequestReviewDto
{
    [Required]
    public string Status { get; set; } = string.Empty;

[StringLength(2000)]
    public string? ReviewNotes { get; set; }

}