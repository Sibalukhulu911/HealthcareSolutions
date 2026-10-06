namespace HealthcareSolutions.API.Models;

public class MedicalRecord
{
    public int MedicalRecordId { get; set; }
    public int PatientId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // Navigation property
    public Patient Patient { get; set; } = null!;

    public ICollection<VisitNote> VisitNotes { get; set; }
        = new List<VisitNote>();
    public ICollection<Prescription> Prescriptions { get; set; }
        = new List<Prescription>();
}
