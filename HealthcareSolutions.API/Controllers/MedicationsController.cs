using HealthcareSolutions.API.DTOs.Medication;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicationsController : ControllerBase
{
    private readonly IMedicationService _medicationService;

    public MedicationsController(
        IMedicationService medicationService)
    {
        _medicationService = medicationService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<IEnumerable<MedicationResponseDto>>>
        GetAll()
    {
        var medications =
            await _medicationService.GetAllAsync();

        return Ok(medications);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<MedicationResponseDto>>
        GetById(int id)
    {
        var medication =
            await _medicationService.GetByIdAsync(id);

        if (medication == null)
        {
            return NotFound();
        }

        return Ok(medication);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<ActionResult<MedicationResponseDto>>
        Create(MedicationCreateDto dto)
    {
        try
        {
            var medication =
                await _medicationService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = medication.MedicationId },
                medication);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> Update(
        int id,
        MedicationUpdateDto dto)
    {
        try
        {
            var updated =
                await _medicationService.UpdateAsync(
                    id,
                    dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await _medicationService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
}