using System.Text.Json.Serialization;
using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.Prescription;

public class PrescriptionResponseDto
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PrescriptionStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DispensedAt { get; set; }
}