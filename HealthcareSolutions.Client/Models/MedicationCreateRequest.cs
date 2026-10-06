using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class MedicationCreateRequest
{
    [Required(ErrorMessage = "Medication name is required.")]
    [StringLength(200)]
    public string MedicationName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Dosage is required.")]
    [StringLength(100)]
    public string Dosage { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;
}
