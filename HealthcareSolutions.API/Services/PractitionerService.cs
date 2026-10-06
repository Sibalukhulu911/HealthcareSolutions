using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.Practitioner;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class PractitionerService : IPractitionerService
{
    private readonly ApplicationDbContext _context;

    public PractitionerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PractitionerResponseDto>> GetAllAsync()
    {
        return await _context.Practitioners
            .AsNoTracking()
            .Select(p => new PractitionerResponseDto
            {
                PractitionerId = p.PractitionerId,
                UserId = p.UserId,
                PractitionerFirstName = p.PractitionerFirstName,
                PractitionerLastName = p.PractitionerLastName,
                Specialty = p.Specialty,
                OfficeNumber = p.OfficeNumber,
                IsAvailable = p.IsAvailable
            })
            .ToListAsync();
    }

    public async Task<PractitionerResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Practitioners
            .AsNoTracking()
            .Where(p => p.PractitionerId == id)
            .Select(p => new PractitionerResponseDto
            {
                PractitionerId = p.PractitionerId,
                UserId = p.UserId,
                PractitionerFirstName = p.PractitionerFirstName,
                PractitionerLastName = p.PractitionerLastName,
                Specialty = p.Specialty,
                OfficeNumber = p.OfficeNumber,
                IsAvailable = p.IsAvailable
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PractitionerResponseDto> CreateAsync(PractitionerCreateDto dto)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.UserId == dto.UserId);

        if (!userExists)
            throw new InvalidOperationException("The specified user does not exist.");

        var practitionerExists = await _context.Practitioners
            .AnyAsync(p => p.UserId == dto.UserId);

        if (practitionerExists)
            throw new InvalidOperationException("This user is already linked to a practitioner.");

        var practitioner = new Practitioner
        {
            UserId = dto.UserId,
            PractitionerFirstName = dto.PractitionerFirstName,
            PractitionerLastName = dto.PractitionerLastName,
            Specialty = dto.Specialty,
            OfficeNumber = dto.OfficeNumber,
            IsAvailable = dto.IsAvailable
        };

        _context.Practitioners.Add(practitioner);
        await _context.SaveChangesAsync();

        return new PractitionerResponseDto
        {
            PractitionerId = practitioner.PractitionerId,
            UserId = practitioner.UserId,
            PractitionerFirstName = practitioner.PractitionerFirstName,
            PractitionerLastName = practitioner.PractitionerLastName,
            Specialty = practitioner.Specialty,
            OfficeNumber = practitioner.OfficeNumber,
            IsAvailable = practitioner.IsAvailable
        };
    }

    public async Task<bool> UpdateAsync(int id, PractitionerUpdateDto dto)
    {
        var practitioner = await _context.Practitioners.FindAsync(id);

        if (practitioner == null)
            return false;

        practitioner.PractitionerFirstName = dto.PractitionerFirstName;
        practitioner.PractitionerLastName = dto.PractitionerLastName;
        practitioner.Specialty = dto.Specialty;
        practitioner.OfficeNumber = dto.OfficeNumber;
        practitioner.IsAvailable = dto.IsAvailable;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var practitioner = await _context.Practitioners.FindAsync(id);

        if (practitioner == null)
            return false;

        practitioner.IsAvailable = false;

        await _context.SaveChangesAsync();

        return true;
    }
}