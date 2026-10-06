using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HealthcareSolutions.API.Enums;

namespace HealthcareSolutions.API.DTOs.Prescription;

public class PrescriptionUpdateStatusDto
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PrescriptionStatus Status { get; set; }
}
