using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class MedicalRecordCreateRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a patient.")]
    public int PatientId { get; set; }
}