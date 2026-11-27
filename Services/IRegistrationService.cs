using EventRegistrationAPI_ArtemKotliar.DTOs;

namespace EventRegistrationAPI_ArtemKotliar.Services;

public interface IRegistrationService
{
    Task<List<RegistrationDto>> GetByEventIdAsync(int eventId);

    Task<RegistrationDto> CreateAsync(int eventId, CreateRegistrationDto dto);

    Task<bool> DeleteAsync(int id);
}