using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }
    public int MedicalRecordId { get; set; }
    public int PractitionerId { get; set; }
    public int AppointmentId { get; set; }
    public int MedicationId { get; set; }
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public PrescriptionStatus Status { get; set; }
        = PrescriptionStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DispensedAt { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;
    public Practitioner Practitioner { get; set; } = null!;
    public Appointment Appointment { get; set; } = null!;
    public Medication Medication { get; set; } = null!;
}
