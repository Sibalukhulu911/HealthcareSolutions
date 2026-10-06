using System.Security.Claims;
using HealthcareSolutions.API.DTOs.MedicationRequest;
using HealthcareSolutions.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSolutions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicationRequestsController : ControllerBase
{
    private readonly IMedicationRequestService
    _medicationRequestService;

public MedicationRequestsController(
    IMedicationRequestService medicationRequestService)
    {
        _medicationRequestService =
            medicationRequestService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<
        ActionResult<IEnumerable<MedicationRequestResponseDto>>>
        GetAll()
    {
        var requests =
            await _medicationRequestService.GetAllAsync();

        return Ok(requests);
    }

    [HttpGet("practitioner/{practitionerId:int}")]
    [Authorize(Roles = "Practitioner,Admin")]
    public async Task<
        ActionResult<IEnumerable<MedicationRequestResponseDto>>>
        GetByPractitioner(int practitionerId)
    {
        var requests =
            await _medicationRequestService
                .GetByPractitionerIdAsync(
                    practitionerId);

        return Ok(requests);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Practitioner,Pharmacist")]
    public async Task<
        ActionResult<MedicationRequestResponseDto>>
        GetById(int id)
    {
        var request =
            await _medicationRequestService
                .GetByIdAsync(id);

        if (request == null)
            return NotFound();

        return Ok(request);
    }

    [HttpPost]
    [Authorize(Roles = "Practitioner")]
    public async Task<
        ActionResult<MedicationRequestResponseDto>>
        Create(MedicationRequestCreateDto dto)
    {
        try
        {
            var result =
                await _medicationRequestService
                    .CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.MedicationRequestId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}/review")]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<
        ActionResult<MedicationRequestResponseDto>>
        Review(
            int id,
            MedicationRequestReviewDto dto)
    {
        var userIdClaim =
            User.FindFirst(
                ClaimTypes.NameIdentifier);

        if (userIdClaim == null ||
            !int.TryParse(
                userIdClaim.Value,
                out var reviewerUserId))
        {
            return Unauthorized();
        }

        try
        {
            var result =
                await _medicationRequestService
                    .ReviewAsync(
                        id,
                        dto,
                        reviewerUserId);

            return Ok(result);
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