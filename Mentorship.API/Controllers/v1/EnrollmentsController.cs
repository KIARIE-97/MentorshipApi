using MediatR;
using Mentorship.Application.Features.Enrollments.Commands.CreateEnrollment;
using Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;
using Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetEnrollmentById;
using Mentorship.Core.Exceptions;
using Mentorship.Shared.Contracts.v1.Enrollments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentResponse>> GetById(int id)
    {
        var result = await _mediator.Send(new GetEnrollmentByIdQuery { Id = id });
        return result == null ? NotFound() : Ok(result);
    }

     [HttpPost]
    public async Task<ActionResult<EnrollmentResponse>> Create(CreateEnrollmentCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
    
     [HttpPost("{id}/mark-attendance")]
    public async Task<IActionResult> MarkAttendance(int id, [FromBody] MarkAttendanceCommand command)
    {
        if (id != command.EnrollmentId)
            return BadRequest();
            
        var result = await _mediator.Send(command);
        return result ? Ok() : NotFound();
    }

    // Complete enrollment
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteEnrollmentCommand command)
    {
        if (id != command.EnrollmentId)
            return BadRequest();
            
        try
        {
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Drop enrollment
    [HttpPost("{id}/drop")]
    public async Task<IActionResult> Drop(int id, [FromBody] DropEnrollmentCommand command)
    {
        if (id != command.EnrollmentId)
            return BadRequest();
            
        try
        {
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    }
}
