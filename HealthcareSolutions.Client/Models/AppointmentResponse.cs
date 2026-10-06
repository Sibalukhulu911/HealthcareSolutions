using System.Text.Json.Serialization;

namespace HealthcareSolutions.Client.Models;

public class AppointmentResponse
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int PractitionerId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int Status { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Notes { get; set; }
}