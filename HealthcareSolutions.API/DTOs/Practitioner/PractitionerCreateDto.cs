using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Practitioner;

public class PractitionerCreateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    [MaxLength(100)]
    public string PractitionerFirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string PractitionerLastName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Specialty { get; set; } = string.Empty;
    [Required]
    [MaxLength(30)]
    public string OfficeNumber { get; set; } = string.Empty;
    
    public bool IsAvailable { get; set; } = true;

}
