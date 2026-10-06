using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class MedicationRequestReviewRequest
{
    [Required]
    public string Status { get; set; } = string.Empty;

    public string? ReviewNotes { get; set; }


}