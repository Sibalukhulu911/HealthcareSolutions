namespace HealthcareSolutions.API.Models;

public class Practitioner
{
    public int PractitionerId { get; set; }
    public int UserId { get; set; }
    public string PractitionerFirstName { get; set; } = string.Empty;
    public string PractitionerLastName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string OfficeNumber { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public User User { get; set; } = null!;

    public ICollection <Appointment> Appointments { get; set; }
        = new List<Appointment>();

    public ICollection<VisitNote> VisitNotes { get; set; }
        = new List<VisitNote>();
    public ICollection<Prescription> Prescriptions { get; set; }
        = new List<Prescription>();
}