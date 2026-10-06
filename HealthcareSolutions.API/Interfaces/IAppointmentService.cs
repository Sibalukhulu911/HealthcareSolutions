using HealthcareSolutions.API.DTOs.Appointment;

namespace HealthcareSolutions.API.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponseDto>> GetAllAsync();

    Task<IEnumerable<AppointmentResponseDto>>
        GetByPractitionerIdAsync(int practitionerId);

    Task<AppointmentResponseDto?> GetByIdAsync(int id);

    Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto);

    Task<bool> UpdateAsync(int id, AppointmentUpdateDto dto);

    Task<bool> CancelAsync(int id);
}