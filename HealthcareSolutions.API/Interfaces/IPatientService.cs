using HealthcareSolutions.API.DTOs.Patient;

namespace HealthcareSolutions.API.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponseDto>> GetAllAsync();

    Task<PatientResponseDto?> GetByIdAsync(int id);

    Task<PatientResponseDto> CreateAsync(PatientCreateDto dto);

    Task<bool> UpdateAsync(int id, PatientUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}