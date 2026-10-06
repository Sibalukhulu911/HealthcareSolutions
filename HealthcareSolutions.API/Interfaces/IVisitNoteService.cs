using HealthcareSolutions.API.DTOs.VisitNote;

namespace HealthcareSolutions.API.Interfaces;

public interface IVisitNoteService
{
    Task<IEnumerable<VisitNoteResponseDto>> GetByMedicalRecordIdAsync(int medicalRecordId);

    Task<VisitNoteResponseDto?> GetByIdAsync(int id);

    Task<VisitNoteResponseDto> CreateAsync(VisitNoteCreateDto dto);

    Task<bool> UpdateAsync(int id, VisitNoteUpdateDto dto);
}