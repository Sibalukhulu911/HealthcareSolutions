using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.Appointment;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Enums;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSolutions.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    private static readonly TimeSpan AppointmentDuration =
        TimeSpan.FromHours(1);

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync()
    {
        return await _context.Appointments
            .AsNoTracking()
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PractitionerId = a.PractitionerId,
                ScheduledAt = a.ScheduledAt,
                Status = a.Status,
                Reason = a.Reason,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.AppointmentId == id)
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PractitionerId = a.PractitionerId,
                ScheduledAt = a.ScheduledAt,
                Status = a.Status,
                Reason = a.Reason,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AppointmentResponseDto> CreateAsync(
        AppointmentCreateDto dto)
    {
        await ValidateAppointmentReferences(dto.PatientId, dto.PractitionerId);

        var overlap = await HasOverlapAsync(
            dto.PractitionerId,
            dto.ScheduledAt,
            null);

        if (overlap)
        {
            throw new InvalidOperationException(
                "The practitioner already has an appointment during this time.");
        }

        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            PractitionerId = dto.PractitionerId,
            ScheduledAt = dto.ScheduledAt,
            Reason = dto.Reason,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return MapToDto(appointment);
    }

    public async Task<IEnumerable<AppointmentResponseDto>>
        GetByPractitionerIdAsync(int practitionerId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.PractitionerId == practitionerId)
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PractitionerId = a.PractitionerId,
                ScheduledAt = a.ScheduledAt,
                Status = a.Status,
                Reason = a.Reason,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
               

            })
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(
        int id,
        AppointmentUpdateDto dto)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return false;

        if (appointment.Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException(
                "A cancelled appointment cannot be updated.");

        var practitionerExists = await _context.Practitioners
            .AnyAsync(p => p.PractitionerId == dto.PractitionerId);

        if (!practitionerExists)
            throw new InvalidOperationException(
                "The specified practitioner does not exist.");

        var overlap = await HasOverlapAsync(
            dto.PractitionerId,
            dto.ScheduledAt,
            id);

        if (overlap)
        {
            throw new InvalidOperationException(
                "The practitioner already has an appointment during this time.");
        }

        appointment.PractitionerId = dto.PractitionerId;
        appointment.ScheduledAt = dto.ScheduledAt;
        appointment.Reason = dto.Reason;
        appointment.Status = dto.Status;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return false;

        appointment.Status = AppointmentStatus.Cancelled;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    private async Task ValidateAppointmentReferences(
        int patientId,
        int practitionerId)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == patientId);

        if (!patientExists)
            throw new InvalidOperationException(
                "The specified patient does not exist.");

        var practitionerExists = await _context.Practitioners
            .AnyAsync(p => p.PractitionerId == practitionerId);

        if (!practitionerExists)
            throw new InvalidOperationException(
                "The specified practitioner does not exist.");
    }

    private async Task<bool> HasOverlapAsync(
    int practitionerId,
    DateTime scheduledAt,
    int? excludedAppointmentId)
    {
        var requestedEnd = scheduledAt.Add(AppointmentDuration);

        var appointments = await _context.Appointments
            .Where(a =>
                a.PractitionerId == practitionerId &&
                a.Status != AppointmentStatus.Cancelled &&
                (!excludedAppointmentId.HasValue ||
                 a.AppointmentId != excludedAppointmentId.Value))
            .Select(a => a.ScheduledAt)
            .ToListAsync();

        return appointments.Any(existingStart =>
        {
            var existingEnd = existingStart.Add(AppointmentDuration);

            return scheduledAt < existingEnd &&
                   requestedEnd > existingStart;
        });
    }

    private static AppointmentResponseDto MapToDto(
        Appointment appointment)
    {
        return new AppointmentResponseDto
        {
            AppointmentId = appointment.AppointmentId,
            PatientId = appointment.PatientId,
            PractitionerId = appointment.PractitionerId,
            ScheduledAt = appointment.ScheduledAt,
            Status = appointment.Status,
            Reason = appointment.Reason,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }
}