using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Medication;

public class MedicationCreateDto
{
    [Required]
    [MaxLength(150)]
    public string MedicationName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(100)]
    public string Dosage
    { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;
}
