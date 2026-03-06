using MediatR;
using Mentorship.Application.Features.Programs.Commands.CreateProgram;
using Mentorship.Application.Features.Programs.Queries.GetProgram;
using Mentorship.Shared.Contracts.v1.Programs;
using Microsoft.AspNetCore.Mvc;

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

}

