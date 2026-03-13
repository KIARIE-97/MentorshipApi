using MediatR;
using Mentorship.Application.Features.Sessions.Queries.GetSession.GetAllSessions;
using Mentorship.Application.Features.Sessions.Queries.GetSession.GetSessionById;
using Mentorship.Shared.Contracts.v1.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.API.Controllers.v1
{
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
    }
}
