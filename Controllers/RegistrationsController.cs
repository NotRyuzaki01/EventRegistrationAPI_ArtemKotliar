using EventRegistrationAPI_ArtemKotliar.DTOs;
using EventRegistrationAPI_ArtemKotliar.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_ArtemKotliar.Controllers;

[ApiController]
[Route("api")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    // GET /api/events/3/registrations
    [HttpGet("events/{id:int}/registrations")]
    public async Task<IActionResult> GetByEventId(int id)
    {
        try
        {
            var registrations = await _registrationService.GetByEventIdAsync(id);
            return Ok(registrations);
        }
        catch (RegistrationException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    // POST /api/events/2/registrations
    [HttpPost("events/{id:int}/registrations")]
    public async Task<IActionResult> Create(int id, CreateRegistrationDto dto)
    {
        try
        {
            var registration = await _registrationService.CreateAsync(id, dto);
            return StatusCode(201, registration);
        }
        catch (RegistrationException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    // DELETE /api/registrations/1
    [HttpDelete("registrations/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _registrationService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}