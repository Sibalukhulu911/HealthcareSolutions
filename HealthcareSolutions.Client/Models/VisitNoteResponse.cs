namespace HealthcareSolutions.Client.Models;

public class VisitNoteResponse
{
    public int VisitNoteId { get; set; }

    public int MedicalRecordId { get; set; }

    public int PractitionerId { get; set; }

    public int AppointmentId { get; set; }

    public string Notes { get; set; } = string.Empty;

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public DateTime CreatedAt { get; set; }
}

