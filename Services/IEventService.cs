using EventRegistrationAPI_ArtemKotliar.DTOs;

namespace EventRegistrationAPI_ArtemKotliar.Services;

/// <summary>
/// Interface for the EventService. Promising functionality. 
/// </summary>
public interface IEventService
{
    Task<List<EventDto>> GetAllAsync();

    Task<EventDto?> GetByIdAsync(int id);
    
    Task<EventDto> CreateAsync(CreateEventDto dto);

}