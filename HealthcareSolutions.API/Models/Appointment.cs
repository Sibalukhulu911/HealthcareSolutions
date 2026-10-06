    using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int PractitionerId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public AppointmentStatus Status { get; set; }
        = AppointmentStatus.Scheduled;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public Practitioner Practitioner { get; set; } = null!;
    public VisitNote? VisitNote { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
        = new List<Prescription>();
}