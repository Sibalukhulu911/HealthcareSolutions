using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class PrescriptionCreateRequest
{
    [Required]
    public int MedicalRecordId { get; set; }

    [Required]
    public int PractitionerId { get; set; }

    [Required]
    public int AppointmentId { get; set; }

    [Required]
    public int MedicationId { get; set; }

    [Required(ErrorMessage = "Dosage is required.")]
    public string Dosage { get; set; } = string.Empty;

    [Required(ErrorMessage = "Frequency is required.")]
    public string Frequency { get; set; } = string.Empty;

    [Required(ErrorMessage = "Duration is required.")]
    public string Duration { get; set; } = string.Empty;

    public string? Instructions { get; set; }
}