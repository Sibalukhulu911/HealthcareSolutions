namespace HealthcareSolutions.API.DTOs.MedicationRequest;

public class MedicationRequestResponseDto
{
    public int MedicationRequestId { get; set; }

    public int PractitionerId { get; set; }

    public string PractitionerName { get; set; } = string.Empty;

    public string MedicationName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public string Status { get; set; } = string.Empty;

    public int? ReviewedByUserId { get; set; }

    public string? ReviewNotes { get; set; }

    public int? ApprovedMedicationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ReviewedAt { get; set; }

}