using EventRegistrationAPI_ArtemKotliar.DTOs;

namespace EventRegistrationAPI_ArtemKotliar.Services;

/// <summary>
/// Interface for the RegistrationService. Promising functionality. 
/// </summary>
public interface IRegistrationService
{
    Task<List<RegistrationDto>> GetByEventIdAsync(int eventId);

    Task<RegistrationDto> CreateAsync(int eventId, CreateRegistrationDto dto);

    Task<bool> DeleteAsync(int id);
}