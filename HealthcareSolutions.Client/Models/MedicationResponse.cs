namespace HealthcareSolutions.Client.Models;

public class MedicationResponse
{
    public int MedicationId { get; set; }

    public string MedicationName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Dosage { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
}