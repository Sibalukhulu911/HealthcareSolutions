using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class AppointmentCreateRequest
{
    [Range(1, int.MaxValue,
        ErrorMessage = "Please select a patient.")]
    public int PatientId { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Please select a practitioner.")]
    public int PractitionerId { get; set; }

    [Required(ErrorMessage = "Appointment date and time are required.")]
    public DateTime ScheduledAt { get; set; }

    [Required(ErrorMessage = "Appointment reason is required.")]
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;
}