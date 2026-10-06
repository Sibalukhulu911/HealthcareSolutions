using HealthcareSolutions.API.DTOs.Prescription;

namespace HealthcareSolutions.API.Interfaces;

public interface IPrescriptionService
{
    Task<IEnumerable<PrescriptionResponseDto>> GetByMedicalRecordIdAsync(int medicalRecordId);

    Task<PrescriptionResponseDto?> GetByIdAsync(int id);

    Task<PrescriptionResponseDto> CreateAsync(PrescriptionCreateDto dto);

    Task<bool> UpdateStatusAsync(int id, PrescriptionUpdateStatusDto dto);
}