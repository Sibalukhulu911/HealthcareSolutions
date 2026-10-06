using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.Client.Models;

public class VisitNoteUpdateRequest
{
    [Required(ErrorMessage = "Notes are required.")]
    [StringLength(2000)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Diagnosis { get; set; }

    [StringLength(2000)]
    public string? Treatment { get; set; }
}
