using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.MedicalRecord;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MedicalRecordResponseDto?> GetByIdAsync(int id)
    {
        return await _context.MedicalRecords
            .AsNoTracking()
            .Where(m => m.MedicalRecordId == id)
            .Select(m => new MedicalRecordResponseDto
            {
                MedicalRecordId = m.MedicalRecordId,
                PatientId = m.PatientId,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MedicalRecordResponseDto?> GetByPatientIdAsync(
        int patientId)
    {
        return await _context.MedicalRecords
            .AsNoTracking()
            .Where(m => m.PatientId == patientId)
            .Select(m => new MedicalRecordResponseDto
            {
                MedicalRecordId = m.MedicalRecordId,
                PatientId = m.PatientId,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MedicalRecordResponseDto> CreateAsync(
        MedicalRecordCreateDto dto)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == dto.PatientId);

        if (!patientExists)
            throw new InvalidOperationException(
                "The specified patient does not exist.");

        var recordExists = await _context.MedicalRecords
            .AnyAsync(m => m.PatientId == dto.PatientId);

        if (recordExists)
            throw new InvalidOperationException(
                "The patient already has a medical record.");

        var record = new MedicalRecord
        {
            PatientId = dto.PatientId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync();

        return new MedicalRecordResponseDto
        {
            MedicalRecordId = record.MedicalRecordId,
            PatientId = record.PatientId,
            CreatedAt = record.CreatedAt,
            UpdatedAt = record.UpdatedAt
        };
    }
}