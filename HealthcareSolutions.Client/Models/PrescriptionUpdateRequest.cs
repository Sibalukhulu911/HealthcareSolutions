using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class PrescriptionUpdateStatusRequest
{
    [Required]
    public string Status { get; set; } = string.Empty;
}
