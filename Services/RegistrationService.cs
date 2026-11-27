using EventRegistrationAPI_ArtemKotliar.Data;
using EventRegistrationAPI_ArtemKotliar.DTOs;
using EventRegistrationAPI_ArtemKotliar.Model;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_ArtemKotliar.Services;

public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _context;

    public RegistrationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RegistrationDto>> GetByEventIdAsync(int eventId)
    {
        var campusEvent = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == eventId);

        if (campusEvent == null)
        {
            throw new RegistrationException(404, "Event not found.");
        }

        var results = new List<RegistrationDto>();

        foreach (var registration in campusEvent.Registrations)
        {
            results.Add(new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,
                EventTitle = campusEvent.Title,
                StudentName = registration.StudentName,
                Email = registration.Email,
                RegisteredOn = registration.RegisteredOn
            });
        }

        return results;
    }

    public async Task<RegistrationDto> CreateAsync(
        int eventId, CreateRegistrationDto dto)
    {
        var campusEvent = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == eventId);
        
        // 1. The event must exist.
        if (campusEvent == null)
        {
            throw new RegistrationException(404, "Event not found.");
        }

        // 2. The event must not have passed.
        if (campusEvent.EventDate <= DateTime.UtcNow)
        {
            throw new RegistrationException(400, "Registration is closed for past events.");
        }

        // 3. The email must not already be registered.
        var email = dto.Email.Trim();

        var duplicate = campusEvent.Registrations.Any(r => r.Email.Trim().Equals(email, StringComparison.OrdinalIgnoreCase));

        if (duplicate)
        {
            throw new RegistrationException(409, "This email is already registered for this event.");
        }

        // 4. The event must have space.
        if (campusEvent.Registrations.Count >= campusEvent.Capacity)
        {
            throw new RegistrationException(409, "This event is full.");
        }

        var registration = new Registration
        {
            EventId = eventId,
            StudentName = dto.StudentName.Trim(),
            Email = email,
            RegisteredOn = DateTime.UtcNow
        };

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();

        return new RegistrationDto
        {
            RegistrationId = registration.RegistrationId,
            EventTitle = campusEvent.Title,
            StudentName = registration.StudentName,
            Email = registration.Email,
            RegisteredOn = registration.RegisteredOn
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var registration = await _context.Registrations.FindAsync(id);

        if (registration == null)
        {
            return false;
        }

        _context.Registrations.Remove(registration);
        await _context.SaveChangesAsync();

        return true;
    }
}