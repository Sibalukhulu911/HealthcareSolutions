using HealthcareSolutions.API.DTOs.Medication;

namespace HealthcareSolutions.API.Interfaces;

public interface IMedicationService
{
    Task<IEnumerable<MedicationResponseDto>> GetAllAsync();

    Task<MedicationResponseDto?> GetByIdAsync(int id);

    Task<MedicationResponseDto> CreateAsync(MedicationCreateDto dto);

    Task<bool> UpdateAsync(int id, MedicationUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}