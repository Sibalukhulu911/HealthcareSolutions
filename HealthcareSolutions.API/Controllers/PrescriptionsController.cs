using HealthcareSolutions.API.DTOs.Prescription;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Practitioner,Pharmacist,Admin")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(
        IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet("medical-record/{medicalRecordId:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<IEnumerable<PrescriptionResponseDto>>>
        GetByMedicalRecordId(int medicalRecordId)
    {
        var prescriptions =
            await _prescriptionService
                .GetByMedicalRecordIdAsync(medicalRecordId);

        return Ok(prescriptions);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<PrescriptionResponseDto>> GetById(int id)
    {
        var prescription =
            await _prescriptionService.GetByIdAsync(id);

        if (prescription == null)
            return NotFound();

        return Ok(prescription);
    }

    [HttpPost]
    [Authorize(Roles = "Practitioner")]
    public async Task<ActionResult<PrescriptionResponseDto>> Create(
        PrescriptionCreateDto dto)
    {
        try
        {
            var prescription =
                await _prescriptionService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = prescription.PrescriptionId },
                prescription);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}/status")]
    [Authorize(Roles = "Pharmacist")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        PrescriptionUpdateStatusDto dto)
    {
        var updated =
            await _prescriptionService.UpdateStatusAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }
}