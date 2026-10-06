using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.MedicalRecord;

public class MedicalRecordCreateDto
{
    [Required]
    public int PatientId { get; set; }
}