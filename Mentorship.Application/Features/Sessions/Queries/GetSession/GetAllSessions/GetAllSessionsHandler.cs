using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Sessions;

namespace Mentorship.Application.Features.Sessions.Queries.GetSession.GetAllSessions;

public class GetAllSessionsHandler(ISessionRepository repository): IRequestHandler<GetAllSessionsQuery, List<SessionResponse>>
{
    private readonly ISessionRepository _repository = repository;
    public async Task<List<SessionResponse>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetAllAsync();

        return sessions.Select(session => new SessionResponse
        {
            Id = session.Id,
            Title = session.Title,
            Description =session.Description,
            ScheduleAt = session.ScheduleAt,
            DurationMinutes = session.DurationMinutes,
        }).ToList();

    }
}
