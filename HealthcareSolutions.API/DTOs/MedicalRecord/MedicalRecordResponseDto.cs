namespace HealthcareSolutions.API.DTOs.MedicalRecord;

public class MedicalRecordResponseDto
{
    public int MedicalRecordId { get; set; }

    public int PatientId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
