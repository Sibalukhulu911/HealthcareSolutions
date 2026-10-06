namespace HealthcareSolutions.API.DTOs.Practitioner;

public class PractitionerResponseDto
{
    public int PractitionerId { get; set; }
    public int UserId { get; set; }
    public string PractitionerFirstName { get; set; } = string.Empty;
    public string PractitionerLastName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string OfficeNumber { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}
