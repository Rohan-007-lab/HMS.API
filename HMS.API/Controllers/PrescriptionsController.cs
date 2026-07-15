using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HMS.Application.Features.Prescriptions.Commands;
using HMS.Application.Features.Prescriptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;


namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPrescriptionsQuery());
        return Ok(result);
    }

    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetPrescriptionsByPatientQuery { PatientId = patientId });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")] // only doctors issue prescriptions
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByPatient), new { patientId = result.PatientId }, result);
    }
}
