using EventRegistrationAPI_ArtemKotliar.DTOs;

namespace EventRegistrationAPI_ArtemKotliar.Services;

public interface IEventService
{
    Task<List<EventDto>> GetAllAsync();

    Task<EventDto?> GetByIdAsync(int id);
    
    Task<EventDto> CreateAsync(CreateEventDto dto);

}