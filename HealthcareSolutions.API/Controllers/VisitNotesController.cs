using HealthcareSolutions.API.DTOs.VisitNote;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VisitNotesController : ControllerBase
{
    private readonly IVisitNoteService _visitNoteService;

    public VisitNotesController(
        IVisitNoteService visitNoteService)
    {
        _visitNoteService = visitNoteService;
    }

    [HttpGet("medical-record/{medicalRecordId:int}")]
    [Authorize(Roles = "Admin,Practitioner")]
    public async Task<ActionResult<IEnumerable<VisitNoteResponseDto>>>
        GetByMedicalRecordId(int medicalRecordId)
    {
        var notes =
            await _visitNoteService.GetByMedicalRecordIdAsync(
                medicalRecordId);

        return Ok(notes);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Practitioner")]
    public async Task<ActionResult<VisitNoteResponseDto>>
        GetById(int id)
    {
        var note =
            await _visitNoteService.GetByIdAsync(id);

        if (note == null)
            return NotFound();

        return Ok(note);
    }

    [HttpPost]
    [Authorize(Roles = "Practitioner")]
    public async Task<ActionResult<VisitNoteResponseDto>>
        Create(VisitNoteCreateDto dto)
    {
        try
        {
            var note =
                await _visitNoteService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = note.VisitNoteId },
                note);
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
    [Authorize(Roles = "Practitioner")]
    public async Task<IActionResult> Update(
        int id,
        VisitNoteUpdateDto dto)
    {
        try
        {
            var updated =
                await _visitNoteService.UpdateAsync(id, dto);

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
}

