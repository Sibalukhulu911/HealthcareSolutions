using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.VisitNote;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class VisitNoteService : IVisitNoteService
{
    private readonly ApplicationDbContext _context;

    public VisitNoteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VisitNoteResponseDto>>
        GetByMedicalRecordIdAsync(int medicalRecordId)
    {
        return await _context.VisitNotes
            .AsNoTracking()
            .Where(v => v.MedicalRecordId == medicalRecordId)
            .Select(v => new VisitNoteResponseDto
            {
                VisitNoteId = v.VisitNoteId,
                MedicalRecordId = v.MedicalRecordId,
                PractitionerId = v.PractitionerId,
                AppointmentId = v.AppointmentId,
                Notes = v.Notes,
                Diagnosis = v.Diagnosis,
                Treatment = v.Treatment,
                CreatedAt = v.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<VisitNoteResponseDto?> GetByIdAsync(int id)
    {
        return await _context.VisitNotes
            .AsNoTracking()
            .Where(v => v.VisitNoteId == id)
            .Select(v => new VisitNoteResponseDto
            {
                VisitNoteId = v.VisitNoteId,
                MedicalRecordId = v.MedicalRecordId,
                PractitionerId = v.PractitionerId,
                AppointmentId = v.AppointmentId,
                Notes = v.Notes,
                Diagnosis = v.Diagnosis,
                Treatment = v.Treatment,
                CreatedAt = v.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<VisitNoteResponseDto> CreateAsync(
        VisitNoteCreateDto dto)
    {
        var medicalRecordExists = await _context.MedicalRecords
            .AnyAsync(m => m.MedicalRecordId == dto.MedicalRecordId);

        if (!medicalRecordExists)
            throw new InvalidOperationException(
                "The specified medical record does not exist.");

        var practitionerExists = await _context.Practitioners
            .AnyAsync(p => p.PractitionerId == dto.PractitionerId);

        if (!practitionerExists)
            throw new InvalidOperationException(
                "The specified practitioner does not exist.");

        var appointmentExists = await _context.Appointments
            .AnyAsync(a => a.AppointmentId == dto.AppointmentId);

        if (!appointmentExists)
            throw new InvalidOperationException(
                "The specified appointment does not exist.");

        var noteExists = await _context.VisitNotes
            .AnyAsync(v => v.AppointmentId == dto.AppointmentId);

        if (noteExists)
            throw new InvalidOperationException(
                "This appointment already has a visit note.");

        var note = new VisitNote
        {
            MedicalRecordId = dto.MedicalRecordId,
            PractitionerId = dto.PractitionerId,
            AppointmentId = dto.AppointmentId,
            Notes = dto.Notes,
            Diagnosis = dto.Diagnosis,
            Treatment = dto.Treatment,
            CreatedAt = DateTime.UtcNow
        };

        _context.VisitNotes.Add(note);
        await _context.SaveChangesAsync();

        return MapToDto(note);
    }

    public async Task<bool> UpdateAsync(
        int id,
        VisitNoteUpdateDto dto)
    {
        var note = await _context.VisitNotes.FindAsync(id);

        if (note == null)
            return false;

        note.Notes = dto.Notes;
        note.Diagnosis = dto.Diagnosis;
        note.Treatment = dto.Treatment;

        await _context.SaveChangesAsync();

        return true;
    }

    private static VisitNoteResponseDto MapToDto(
        VisitNote note)
    {
        return new VisitNoteResponseDto
        {
            VisitNoteId = note.VisitNoteId,
            MedicalRecordId = note.MedicalRecordId,
            PractitionerId = note.PractitionerId,
            AppointmentId = note.AppointmentId,
            Notes = note.Notes,
            Diagnosis = note.Diagnosis,
            Treatment = note.Treatment,
            CreatedAt = note.CreatedAt
        };
    }
}