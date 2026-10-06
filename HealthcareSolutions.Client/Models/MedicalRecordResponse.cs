namespace HealthcareSolutions.Client.Models;

public class MedicalRecordResponse
{
    public int MedicalRecordId { get; set; }
    public int PatientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}