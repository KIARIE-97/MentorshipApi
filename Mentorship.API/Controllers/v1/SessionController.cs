using MediatR;
using Mentorship.Application.Features.Sessions.Commands.CreateSession;
using Mentorship.Application.Features.Sessions.Commands.DeleteSession;
using Mentorship.Application.Features.Sessions.Commands.UpdateSession;
using Mentorship.Application.Features.Sessions.Queries.GetSession.GetAllSessions;
using Mentorship.Application.Features.Sessions.Queries.GetSession.GetSessionById;
using Mentorship.Shared.Contracts.v1.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.API.Controllers.v1;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SessionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SessionResponse>>> GetAll()
        {
            var query = new GetAllSessionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SessionResponse>> Get(int id)
    {
        var query = new GetSessionQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateSessionRequest request)
    {
        var command = CreateSessionCommand.FromRequest(request);
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update (int id, UpdateSessionCommand command)
    {
        if(id != command.Id)
        {
            return BadRequest("ID in URL does not match ID in command");
        }
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> Delete (int id)
    {
        var command = new DeleteSessionCommand(id);
        
        // Send through MediatR
        var result = await _mediator.Send(command);
        
        // Return appropriate response
        if (!result)
        {
            return NotFound($"session with ID {id} not found");
        }
        
        return NoContent();    }

    }

