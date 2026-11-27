using EventRegistrationAPI_ArtemKotliar.DTOs;
using EventRegistrationAPI_ArtemKotliar.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_ArtemKotliar.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    // GET /api/events
    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetAll()
    {
        var events = await _eventService.GetAllAsync();

        return Ok(events);
    }

    // GET /api/events/3
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventDto>> GetById(int id)
    {
        var campusEvent = await _eventService.GetByIdAsync(id);

        if (campusEvent == null)
        {
            return NotFound();
        }

        return Ok(campusEvent);
    }
    
    // POST /api/events
    [HttpPost]
    public async Task<ActionResult<EventDto>> Create(CreateEventDto dto)
    {
        var createdEvent = await _eventService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdEvent.EventId },
            createdEvent);
    }   
}