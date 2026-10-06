using HealthcareSolutions.API.DTOs.MedicationRequest;

namespace HealthcareSolutions.API.Interfaces;

public interface IMedicationRequestService
{
    Task<IEnumerable<MedicationRequestResponseDto>> GetAllAsync();

Task<IEnumerable<MedicationRequestResponseDto>>
    GetByPractitionerIdAsync(int practitionerId);

    Task<MedicationRequestResponseDto?> GetByIdAsync(int id);

    Task<MedicationRequestResponseDto>
        CreateAsync(MedicationRequestCreateDto dto);

    Task<MedicationRequestResponseDto>
        ReviewAsync(
            int id,
            MedicationRequestReviewDto dto,
            int reviewerUserId);
}