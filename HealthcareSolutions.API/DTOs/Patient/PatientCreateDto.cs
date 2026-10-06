using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Patient;

//This DTO is used for creating a new patient record.
//It contains the necessary fields required for patient creation,
//along with validation attributes to ensure data integrity.

public class PatientCreateDto
{
    [Required]
    [MaxLength(100)]
    public string PatientFirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string PatientLastName { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(50)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(300)]
    public string? Address { get; set; }
}