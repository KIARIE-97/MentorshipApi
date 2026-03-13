using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Sessions;

namespace Mentorship.Application.Features.Sessions.Queries.GetSession.GetSessionById;

public class GetSessionHandler(ISessionRepository repository): IRequestHandler<GetSessionQuery, SessionResponse>
{
    private readonly ISessionRepository _repository = repository;

     public async Task<SessionResponse?> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(request.Id);

        if(session == null) return null;

        return new SessionResponse
        {
            Id = session.Id,
            Title = session.Title,
            Description = session.Description,
            ScheduleAt = session.ScheduleAt,
            DurationMinutes = session.DurationMinutes,
            ProgramId = session.ProgramId,
        };
    }
}
