using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.Models;

public class MedicationRequest
{
    public int MedicationRequestId { get; set; }

    public int PractitionerId { get; set; }

    public string MedicationName { get; set; } = string.Empty;

    public string Dosage { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public MedicationRequestStatus Status { get; set; }
        = MedicationRequestStatus.Pending;

    public int? ReviewedByUserId { get; set; }

    public string? ReviewNotes { get; set; }

    public int? ApprovedMedicationId { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }

    public Practitioner Practitioner { get; set; } = null!;

    public Medication? ApprovedMedication { get; set; }
}