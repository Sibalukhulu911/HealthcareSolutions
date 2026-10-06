using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.Medication;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class MedicationService : IMedicationService
{
    private readonly ApplicationDbContext _context;

    public MedicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicationResponseDto>> GetAllAsync()
    {
        return await _context.Medications
            .AsNoTracking()
            .Select(m => new MedicationResponseDto
            {
                MedicationId = m.MedicationId,
                MedicationName = m.MedicationName,
                Description = m.Description,
                Dosage = m.Dosage,
                IsAvailable = m.IsAvailable
            })
            .ToListAsync();
    }

    public async Task<MedicationResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Medications
            .AsNoTracking()
            .Where(m => m.MedicationId == id)
            .Select(m => new MedicationResponseDto
            {
                MedicationId = m.MedicationId,
                MedicationName = m.MedicationName,
                Description = m.Description,
                Dosage = m.Dosage,
                IsAvailable = m.IsAvailable
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MedicationResponseDto> CreateAsync(
        MedicationCreateDto dto)
    {
        var medication = new Medication
        {
            MedicationName = dto.MedicationName,
            Description = dto.Description,
            Dosage = dto.Dosage,
            IsAvailable = dto.IsAvailable
        };

        _context.Medications.Add(medication);

        await _context.SaveChangesAsync();

        return MapToDto(medication);
    }

    public async Task<bool> UpdateAsync(
        int id,
        MedicationUpdateDto dto)
    {
        var medication =
            await _context.Medications
                .FindAsync(id);

        if (medication == null)
        {
            return false;
        }

        medication.MedicationName = dto.MedicationName;
        medication.Description = dto.Description;
        medication.Dosage = dto.Dosage;
        medication.IsAvailable = dto.IsAvailable;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medication =
            await _context.Medications
                .FirstOrDefaultAsync(
                    m => m.MedicationId == id);

        if (medication == null)
        {
            return false;
        }

        var hasPrescriptions =
            await _context.Prescriptions
                .AnyAsync(
                    p => p.MedicationId == id);

        if (hasPrescriptions)
        {
            throw new InvalidOperationException(
                "This medication cannot be deleted because it is already associated with one or more prescriptions. Mark the medication as unavailable instead.");
        }

        var hasApprovedMedicationRequests =
            await _context.MedicationRequests
                .AnyAsync(
                    r => r.ApprovedMedicationId == id);

        if (hasApprovedMedicationRequests)
        {
            throw new InvalidOperationException(
                "This medication cannot be deleted because it is associated with an approved medication request. Mark the medication as unavailable instead.");
        }

        _context.Medications.Remove(medication);

        await _context.SaveChangesAsync();

        return true;
    }

    private static MedicationResponseDto MapToDto(
        Medication medication)
    {
        return new MedicationResponseDto
        {
            MedicationId = medication.MedicationId,
            MedicationName = medication.MedicationName,
            Description = medication.Description,
            Dosage = medication.Dosage,
            IsAvailable = medication.IsAvailable
        };
    }
}