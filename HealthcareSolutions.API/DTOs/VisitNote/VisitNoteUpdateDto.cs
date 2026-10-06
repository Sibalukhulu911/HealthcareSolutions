using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.VisitNote;

public class VisitNoteUpdateDto
{
    [Required]
    public string Notes { get; set; } = string.Empty;

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }
}
