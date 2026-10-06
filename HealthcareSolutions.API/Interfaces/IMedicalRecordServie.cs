using HealthcareSolutions.API.DTOs.MedicalRecord;

namespace HealthcareSolutions.API.Interfaces;

public interface IMedicalRecordService
{
    Task<MedicalRecordResponseDto?> GetByIdAsync(int id);

    Task<MedicalRecordResponseDto?> GetByPatientIdAsync(int patientId);

    Task<MedicalRecordResponseDto> CreateAsync(MedicalRecordCreateDto dto);
}