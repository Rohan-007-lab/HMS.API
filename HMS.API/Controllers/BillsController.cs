using HMS.Application.Features.Billing.Commands;
using HMS.Application.Features.Billing.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllBillsQuery());
        return Ok(result);
    }

    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Admin,Receptionist,Patient")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetBillsByPatientQuery { PatientId = patientId });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> Create([FromBody] CreateBillCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByPatient), new { patientId = result.PatientId }, result);
    }

    [HttpPatch("{id}/pay")]
    [Authorize(Roles = "Admin,Receptionist")]
    public async Task<IActionResult> MarkPaid(Guid id)
    {
        var result = await _mediator.Send(new MarkBillPaidCommand { Id = id });
        if (!result) return NotFound();
        return NoContent();
    }
}