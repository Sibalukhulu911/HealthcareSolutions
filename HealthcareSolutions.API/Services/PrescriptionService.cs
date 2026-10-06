using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.Prescription;
using HealthcareSolutions.API.Enums;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly ApplicationDbContext _context;

    public PrescriptionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PrescriptionResponseDto>>
        GetByMedicalRecordIdAsync(int medicalRecordId)
    {
        return await _context.Prescriptions
            .AsNoTracking()
            .Where(p => p.MedicalRecordId == medicalRecordId)
            .Select(p => new PrescriptionResponseDto
            {
                PrescriptionId = p.PrescriptionId,
                MedicalRecordId = p.MedicalRecordId,
                PractitionerId = p.PractitionerId,
                AppointmentId = p.AppointmentId,
                MedicationId = p.MedicationId,
                Dosage = p.Dosage,
                Frequency = p.Frequency,
                Duration = p.Duration,
                Instructions = p.Instructions,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                DispensedAt = p.DispensedAt
            })
            .ToListAsync();
    }

    public async Task<PrescriptionResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Prescriptions
            .AsNoTracking()
            .Where(p => p.PrescriptionId == id)
            .Select(p => new PrescriptionResponseDto
            {
                PrescriptionId = p.PrescriptionId,
                MedicalRecordId = p.MedicalRecordId,
                PractitionerId = p.PractitionerId,
                AppointmentId = p.AppointmentId,
                MedicationId = p.MedicationId,
                Dosage = p.Dosage,
                Frequency = p.Frequency,
                Duration = p.Duration,
                Instructions = p.Instructions,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                DispensedAt = p.DispensedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PrescriptionResponseDto> CreateAsync(
        PrescriptionCreateDto dto)
    {
        var recordExists = await _context.MedicalRecords
            .AnyAsync(m => m.MedicalRecordId == dto.MedicalRecordId);

        if (!recordExists)
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

        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == dto.MedicationId);

        if (medication == null)
            throw new InvalidOperationException(
                "The specified medication does not exist.");

        var prescription = new Prescription
        {
            MedicalRecordId = dto.MedicalRecordId,
            PractitionerId = dto.PractitionerId,
            AppointmentId = dto.AppointmentId,
            MedicationId = dto.MedicationId,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            Duration = dto.Duration,
            Instructions = dto.Instructions,
            Status = PrescriptionStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return MapToDto(prescription);
    }

    public async Task<bool> UpdateStatusAsync(
        int id,
        PrescriptionUpdateStatusDto dto)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);

        if (prescription == null)
            return false;

        if (dto.Status == PrescriptionStatus.Dispensed)
        {
            prescription.DispensedAt = DateTime.UtcNow;
        }

        if (dto.Status == PrescriptionStatus.Unavailable)
        {
            prescription.DispensedAt = null;
        }

        prescription.Status = dto.Status;

        await _context.SaveChangesAsync();

        return true;
    }

    private static PrescriptionResponseDto MapToDto(
        Prescription prescription)
    {
        return new PrescriptionResponseDto
        {
            PrescriptionId = prescription.PrescriptionId,
            MedicalRecordId = prescription.MedicalRecordId,
            PractitionerId = prescription.PractitionerId,
            AppointmentId = prescription.AppointmentId,
            MedicationId = prescription.MedicationId,
            Dosage = prescription.Dosage,
            Frequency = prescription.Frequency,
            Duration = prescription.Duration,
            Instructions = prescription.Instructions,
            Status = prescription.Status,
            CreatedAt = prescription.CreatedAt,
            DispensedAt = prescription.DispensedAt
        };
    }
}