using HealthcareSolutions.API.DTOs.Appointment;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

public AppointmentsController(
    IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Receptionist,Practitioner")]
    public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAll()
    {
        var appointments =
            await _appointmentService.GetAllAsync();

        return Ok(appointments);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist,Practitioner")]
    public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
    {
        var appointment =
            await _appointmentService.GetByIdAsync(id);

        if (appointment == null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpGet("practitioner/{practitionerId:int}")]
    [Authorize(Roles = "Admin,Practitioner")]
    public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>>
        GetByPractitionerId(int practitionerId)
    {
        var appointments =
            await _appointmentService.GetByPractitionerIdAsync(practitionerId);

        return Ok(appointments);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<ActionResult<AppointmentResponseDto>> Create(
        AppointmentCreateDto dto)
    {
        try
        {
            var appointment =
                await _appointmentService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = appointment.AppointmentId },
                appointment);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Update(
        int id,
        AppointmentUpdateDto dto)
    {
        try
        {
            var updated =
                await _appointmentService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Cancel(int id)
    {
        var cancelled =
            await _appointmentService.CancelAsync(id);

        if (!cancelled)
            return NotFound();

        return NoContent();
    }

}
