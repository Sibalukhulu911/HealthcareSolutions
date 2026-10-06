using HealthcareSolutions.API.DTOs.Practitioner;

namespace HealthcareSolutions.API.Interfaces;

public interface IPractitionerService
{
    Task<IEnumerable<PractitionerResponseDto>> GetAllAsync();

    Task<PractitionerResponseDto?> GetByIdAsync(int id);

    Task<PractitionerResponseDto> CreateAsync(PractitionerCreateDto dto);

    Task<bool> UpdateAsync(int id, PractitionerUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}