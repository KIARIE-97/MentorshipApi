using MediatR;
using Mentorship.Application.Features.Programs.Commands.CreateProgram;
using Mentorship.Application.Features.Programs.Queries.GetProgram.GetProgramById;
using Mentorship.Application.Features.Programs.Queries.GetProgram.GetAllPrograms;
using Mentorship.Shared.Contracts.v1.Programs;
using Microsoft.AspNetCore.Mvc;
using Mentorship.Application.Features.Programs.Commands.UpdateProgram;
using Mentorship.Application.Features.Programs.Commands.DeleteProgram;

namespace Mentorship.API.Controllers.v1;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProgramsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
    

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public async Task<ActionResult<int>> Create([FromBody] CreateProgramRequest request)
    {
        var command = CreateProgramCommand.FromRequest(request);
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProgramResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgramResponse>> Get(int id)
    {
        var query = new GetProgramQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProgramResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProgramResponse>>> GetAll()
    {
        var query = new GetAllProgramsQuery(); // You'll need to create this
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("id")]
    [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update (int Id, UpdateProgramCommand command)
    {
        if(Id != command.Id)
        {
            return BadRequest("ID in URL does not match ID in command");
        }
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("id")]
    [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> Delete (int Id)
    {
        var command = new DeleteProgramCommand(Id);
        
        // Send through MediatR
        var result = await _mediator.Send(command);
        
        // Return appropriate response
        if (!result)
        {
            return NotFound($"Program with ID {Id} not found");
        }
        
        return NoContent();    }
}

