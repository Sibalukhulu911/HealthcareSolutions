using HealthcareSolutions.API.DTOs.Practitioner;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PractitionersController : ControllerBase
{
    private readonly IPractitionerService _practitionerService;

    public PractitionersController(
        IPractitionerService practitionerService)
    {
        _practitionerService = practitionerService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Receptionist,Practitioner,Pharmacist")]
    public async Task<ActionResult<IEnumerable<PractitionerResponseDto>>>
        GetAll()
    {
        var practitioners =
            await _practitionerService.GetAllAsync();

        return Ok(practitioners);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist,Practitioner,Pharmacist")]
    public async Task<ActionResult<PractitionerResponseDto>>
        GetById(int id)
    {
        var practitioner =
            await _practitionerService.GetByIdAsync(id);

        if (practitioner == null)
            return NotFound();

        return Ok(practitioner);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PractitionerResponseDto>>
        Create(PractitionerCreateDto dto)
    {
        try
        {
            var practitioner =
                await _practitionerService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = practitioner.PractitionerId },
                practitioner);
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        PractitionerUpdateDto dto)
    {
        try
        {
            var updated =
                await _practitionerService.UpdateAsync(
                    id,
                    dto);

            if (!updated)
                return NotFound();

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
        var deleted =
            await _practitionerService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}