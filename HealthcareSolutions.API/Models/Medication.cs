namespace HealthcareSolutions.API.Models;

public class Medication
    {
    public int MedicationId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;

    public ICollection<Prescription> Prescriptions { get; set; }
        = new List<Prescription>();
}
