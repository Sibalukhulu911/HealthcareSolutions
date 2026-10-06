using HealthcareSolutions.API.DTOs.MedicalRecord;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

public MedicalRecordsController(
    IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<MedicalRecordResponseDto>> GetById(int id)
    {
        var record =
            await _medicalRecordService.GetByIdAsync(id);

        if (record == null)
            return NotFound();

        return Ok(record);
    }

    [HttpGet("patient/{patientId:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<ActionResult<MedicalRecordResponseDto>> GetByPatientId(
        int patientId)
    {
        var record =
            await _medicalRecordService.GetByPatientIdAsync(patientId);

        if (record == null)
            return NotFound();

        return Ok(record);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Practitioner")]
    public async Task<ActionResult<MedicalRecordResponseDto>> Create(
        MedicalRecordCreateDto dto)
    {
        try
        {
            var record =
                await _medicalRecordService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = record.MedicalRecordId },
                record);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
