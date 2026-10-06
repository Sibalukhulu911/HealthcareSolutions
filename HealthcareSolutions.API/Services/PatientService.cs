using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.Patient;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientResponseDto>> GetAllAsync()
    {
        return await _context.Patients
            .AsNoTracking()
            .Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                PatientFirstName = p.PatientFirstName,
                PatientLastName = p.PatientLastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<PatientResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.PatientId == id)
            .Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                PatientFirstName = p.PatientFirstName,
                PatientLastName = p.PatientLastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PatientResponseDto> CreateAsync(PatientCreateDto dto)
    {
        var patient = new Patient
        {
            PatientFirstName = dto.PatientFirstName,
            PatientLastName = dto.PatientLastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return new PatientResponseDto
        {
            PatientId = patient.PatientId,
            PatientFirstName = patient.PatientFirstName,
            PatientLastName = patient.PatientLastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            Address = patient.Address,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, PatientUpdateDto dto)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return false;

        patient.PatientFirstName = dto.PatientFirstName;
        patient.PatientLastName = dto.PatientLastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.Gender = dto.Gender;
        patient.PhoneNumber = dto.PhoneNumber;
        patient.Email = dto.Email;
        patient.Address = dto.Address;
        patient.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return true;
    }
}