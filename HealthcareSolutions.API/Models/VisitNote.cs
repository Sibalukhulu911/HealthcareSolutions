namespace HealthcareSolutions.API.Models;

public class VisitNote
{
    public int VisitNoteId { get; set; }
    public int MedicalRecordId { get; set; }
    public int PractitionerId { get; set; }
    public int AppointmentId { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Navigation property
    public MedicalRecord MedicalRecord { get; set; } = null!;
    public Practitioner Practitioner { get; set; } = null!;
    public Appointment Appointment { get; set; } = null!;
}