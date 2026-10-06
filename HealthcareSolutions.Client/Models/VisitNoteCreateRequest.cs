using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class VisitNoteCreateRequest
{
    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Please select a medical record.")]
    public int MedicalRecordId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Please select a practitioner.")]
    public int PractitionerId { get; set; }

    [Range(
        1,
        int.MaxValue,
        ErrorMessage = "Please select an appointment.")]
    public int AppointmentId { get; set; }

    [Required(ErrorMessage = "Notes are required.")]
    [StringLength(2000)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Diagnosis { get; set; }

    [StringLength(2000)]
    public string? Treatment { get; set; }
}
