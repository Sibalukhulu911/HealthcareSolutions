using System.ComponentModel.DataAnnotations;

namespace HealthcareSolutions.API.DTOs.Patient;

//This DTO is used for updating an existing patient record.
//It contains the fields that can be updated, along with validation attributes to ensure data integrity.

public class PatientUpdateDto
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