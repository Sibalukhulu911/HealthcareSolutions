using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.VisitNote;

public class VisitNoteCreateDto
{
    [Required]
    public int MedicalRecordId { get; set; }

    [Required]
    public int PractitionerId { get; set; }

    [Required]
    public int AppointmentId { get; set; }

    [Required]
    public string Notes { get; set; } = string.Empty;

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }
}