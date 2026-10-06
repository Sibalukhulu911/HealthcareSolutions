using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Appointment;

public class AppointmentCreateDto
{
    [Required]
    public int PatientId { get; set; }
    [Required]
    public int PractitionerId { get; set; }
    [Required]
    public DateTime ScheduledAt { get; set; }
    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
}