using HealthcareSolutions.API.DTOs.User;

namespace HealthcareSolutions.API.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto?> GetByIdAsync(int id);

    Task<UserResponseDto> CreateAsync(UserCreateDto dto);

    Task<bool> UpdateAsync(int id, UserUpdateDto dto);

    Task<bool> DeactivateAsync(int id);
}