using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.MedicationRequest;

public class MedicationRequestCreateDto
{
    [Required]
    public int PractitionerId { get; set; }

    [Required(ErrorMessage = "Medication name is required.")]
    [StringLength(200)]
    public string MedicationName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dosage is required.")]
    [StringLength(100)]
    public string Dosage { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}