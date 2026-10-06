namespace HealthcareSolutions.Client.Models;

public class PrescriptionResponse
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

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? DispensedAt { get; set; }
}
