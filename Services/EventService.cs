using EventRegistrationAPI_ArtemKotliar.Data;
using EventRegistrationAPI_ArtemKotliar.DTOs;
using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_ArtemKotliar.Model;

namespace EventRegistrationAPI_ArtemKotliar.Services;

public class EventService : IEventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> GetAllAsync()
    {
        var events = await _context.Events
            .Include(e => e.Registrations)
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        var results = new List<EventDto>();

        foreach (var campusEvent in events)
        {
            results.Add(new EventDto
            {
                EventId = campusEvent.EventId,
                Title = campusEvent.Title,
                Location = campusEvent.Location,
                EventDate = campusEvent.EventDate,
                Capacity = campusEvent.Capacity,
                RegisteredCount = campusEvent.Registrations.Count,
                SpotsLeft = campusEvent.Capacity
                    - campusEvent.Registrations.Count
            });
        }

        return results;
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var campusEvent = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == id);

        if (campusEvent == null)
        {
            return null;
        }

        return new EventDto
        {
            EventId = campusEvent.EventId,
            Title = campusEvent.Title,
            Location = campusEvent.Location,
            EventDate = campusEvent.EventDate,
            Capacity = campusEvent.Capacity,
            RegisteredCount = campusEvent.Registrations.Count,
            SpotsLeft = campusEvent.Capacity
                - campusEvent.Registrations.Count
        };
    }
    
    public async Task<EventDto> CreateAsync(CreateEventDto dto)
    {
        var campusEvent = new Event
        {
            Title = dto.Title,
            Location = dto.Location,
            EventDate = dto.EventDate,
            Capacity = dto.Capacity
        };

        _context.Events.Add(campusEvent);

        await _context.SaveChangesAsync();

        return new EventDto
        {
            EventId = campusEvent.EventId,
            Title = campusEvent.Title,
            Location = campusEvent.Location,
            EventDate = campusEvent.EventDate,
            Capacity = campusEvent.Capacity,
            RegisteredCount = 0,
            SpotsLeft = campusEvent.Capacity
        };
    }
}