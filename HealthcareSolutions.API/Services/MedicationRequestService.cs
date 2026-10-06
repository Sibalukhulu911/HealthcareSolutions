using HealthcareSolutions.API.Data;
using HealthcareSolutions.API.DTOs.MedicationRequest;
using HealthcareSolutions.API.Enums;
using HealthcareSolutions.API.Interfaces;
using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace HealthcareSolutions.API.Services;

public class MedicationRequestService : IMedicationRequestService
{
    private readonly ApplicationDbContext _context;

    public MedicationRequestService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicationRequestResponseDto>>
        GetAllAsync()
    {
        return await _context.MedicationRequests
            .Include(r => r.Practitioner)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new MedicationRequestResponseDto
            {
                MedicationRequestId =
                    r.MedicationRequestId,

                PractitionerId =
                    r.PractitionerId,

                PractitionerName =
                    r.Practitioner.PractitionerFirstName + " " +
                    r.Practitioner.PractitionerLastName,

                MedicationName =
                    r.MedicationName,

                Dosage =
                    r.Dosage,

                Description =
                    r.Description,

                Notes =
                    r.Notes,

                Status =
                    r.Status.ToString(),

                ReviewedByUserId =
                    r.ReviewedByUserId,

                ReviewNotes =
                    r.ReviewNotes,

                ApprovedMedicationId =
                    r.ApprovedMedicationId,

                CreatedAt =
                    r.CreatedAt,

                ReviewedAt =
                    r.ReviewedAt
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicationRequestResponseDto>>
        GetByPractitionerIdAsync(int practitionerId)
    {
        return await _context.MedicationRequests
            .Include(r => r.Practitioner)
            .Where(r => r.PractitionerId == practitionerId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new MedicationRequestResponseDto
            {
                MedicationRequestId =
                    r.MedicationRequestId,

                PractitionerId =
                    r.PractitionerId,

                PractitionerName =
                    r.Practitioner.PractitionerFirstName + " " +
                    r.Practitioner.PractitionerLastName,

                MedicationName =
                    r.MedicationName,

                Dosage =
                    r.Dosage,

                Description =
                    r.Description,

                Notes =
                    r.Notes,

                Status =
                    r.Status.ToString(),

                ReviewedByUserId =
                    r.ReviewedByUserId,

                ReviewNotes =
                    r.ReviewNotes,

                ApprovedMedicationId =
                    r.ApprovedMedicationId,

                CreatedAt =
                    r.CreatedAt,

                ReviewedAt =
                    r.ReviewedAt
            })
            .ToListAsync();
    }

    public async Task<MedicationRequestResponseDto?>
        GetByIdAsync(int id)
    {
        var request =
            await _context.MedicationRequests
                .Include(r => r.Practitioner)
                .FirstOrDefaultAsync(
                    r => r.MedicationRequestId == id);

        if (request == null)
            return null;

        return MapToDto(request);
    }

    public async Task<MedicationRequestResponseDto>
        CreateAsync(MedicationRequestCreateDto dto)
    {
        var practitioner =
            await _context.Practitioners
                .FirstOrDefaultAsync(
                    p => p.PractitionerId ==
                         dto.PractitionerId);

        if (practitioner == null)
        {
            throw new InvalidOperationException(
                "Practitioner not found.");
        }

        var medicationExists =
            await _context.Medications
                .AnyAsync(m =>
                    m.MedicationName.ToLower() ==
                        dto.MedicationName.ToLower() &&
                    m.Dosage.ToLower() ==
                        dto.Dosage.ToLower());

        if (medicationExists)
        {
            throw new InvalidOperationException(
                "This medication already exists in the catalogue.");
        }

        var pendingRequestExists =
            await _context.MedicationRequests
                .AnyAsync(r =>
                    r.MedicationName.ToLower() ==
                        dto.MedicationName.ToLower() &&
                    r.Dosage.ToLower() ==
                        dto.Dosage. ToLower() &&
                    r.Status ==
                        MedicationRequestStatus.Pending);

        if (pendingRequestExists)
        {
            throw new InvalidOperationException(
                "A pending request already exists for this medication.");
        }

        var request = new MedicationRequest
        {
            PractitionerId =
                dto.PractitionerId,

            MedicationName =
                dto.MedicationName.Trim(),

            Dosage =
                dto.Dosage.Trim(),

            Description =
                dto.Description?.Trim(),

            Notes =
                dto.Notes?.Trim(),

            Status =
                MedicationRequestStatus.Pending,

            CreatedAt =
                DateTime.UtcNow
        };

        _context.MedicationRequests.Add(request);

        await _context.SaveChangesAsync();

        await _context.Entry(request)
            .Reference(r => r.Practitioner)
            .LoadAsync();

        return MapToDto(request);
    }

    public async Task<MedicationRequestResponseDto>
        ReviewAsync(
            int id,
            MedicationRequestReviewDto dto,
            int reviewerUserId)
    {
        var request =
            await _context.MedicationRequests
                .Include(r => r.Practitioner)
                .FirstOrDefaultAsync(
                    r => r.MedicationRequestId == id);

        if (request == null)
        {
            throw new InvalidOperationException(
                "Medication request not found.");
        }

        if (request.Status !=
            MedicationRequestStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending medication requests can be reviewed.");
        }

        if (!Enum.TryParse<MedicationRequestStatus>(
            dto.Status,
            true,
            out var status))
        {
            throw new InvalidOperationException(
                "Invalid medication request status.");
        }

        if (status != MedicationRequestStatus.Approved &&
            status != MedicationRequestStatus.Rejected)
        {
            throw new InvalidOperationException(
                "A request can only be approved or rejected.");
        }

        request.Status =
            status;

        request.ReviewedByUserId =
            reviewerUserId;

        request.ReviewNotes =
            dto.ReviewNotes?.Trim();

        request.ReviewedAt =
            DateTime.UtcNow;

        if (status ==
            MedicationRequestStatus.Approved)
        {
            var existingMedication =
                await _context.Medications
                    .FirstOrDefaultAsync(m =>
                        m.MedicationName.ToLower() ==
                            request.MedicationName.ToLower() &&
                        m.Dosage.ToLower() ==
                            request.Dosage.ToLower());

            if (existingMedication != null)
            {
                request.ApprovedMedicationId =
                    existingMedication.MedicationId;
            }
            else
            {
                var medication = new Medication
                {
                    MedicationName =
                        request.MedicationName,

                    Dosage =
                        request.Dosage,
                    Description =
                        request.Description,

                    IsAvailable =
                        true
                };

                _context.Medications.Add(medication);

                await _context.SaveChangesAsync();

                request.ApprovedMedicationId =
                    medication.MedicationId;
            }
        }

        await _context.SaveChangesAsync();

        return MapToDto(request);
    }

    private static MedicationRequestResponseDto
        MapToDto(MedicationRequest request)
    {
        return new MedicationRequestResponseDto
        {
            MedicationRequestId =
                request.MedicationRequestId,

            PractitionerId =
                request.PractitionerId,

            PractitionerName =
                request.Practitioner.PractitionerFirstName +
                " " +
                request.Practitioner.PractitionerLastName,

            MedicationName =
                request.MedicationName,

            Dosage =
                request.Dosage,

            Description =
                request.Description,

            Notes =
                request.Notes,

            Status =
                request.Status.ToString(),

            ReviewedByUserId =
                request.ReviewedByUserId,

            ReviewNotes =
                request.ReviewNotes,

            ApprovedMedicationId =
                request.ApprovedMedicationId,

            CreatedAt =
                request.CreatedAt,

            ReviewedAt =
                request.ReviewedAt
        };
    }
}
