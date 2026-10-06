using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.Appointment;

public class AppointmentResponseDto
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int PractitionerId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public AppointmentStatus Status { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}