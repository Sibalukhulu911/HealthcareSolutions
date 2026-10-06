using System.ComponentModel.DataAnnotations;
using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.Appointment;

public class AppointmentUpdateDto
{
    [Required]
    public int PractitionerId { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;

    [Required]
    public AppointmentStatus Status { get; set; }
}