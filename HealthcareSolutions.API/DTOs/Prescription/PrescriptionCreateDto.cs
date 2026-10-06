using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Prescription;

public class PrescriptionCreateDto
{
    [Required]
    public int MedicalRecordId { get; set; }

    [Required]
    public int PractitionerId { get; set; }

    [Required]
    public int AppointmentId { get; set; }

    [Required]
    public int MedicationId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Dosage { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Frequency { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Duration { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Instructions { get; set; }
}