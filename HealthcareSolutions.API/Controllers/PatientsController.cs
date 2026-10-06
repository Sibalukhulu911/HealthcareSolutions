using HealthcareSolutions.API.DTOs.Patient;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Receptionist,Practitioner,Pharmacist")]
    public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
    {
        var patients = await _patientService.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist,Practitioner,Pharmacist")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<ActionResult<PatientResponseDto>> Create(
        PatientCreateDto dto)
    {
        var patient = await _patientService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = patient.PatientId },
            patient);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Update(
        int id,
        PatientUpdateDto dto)
    {
        var updated = await _patientService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _patientService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

}
