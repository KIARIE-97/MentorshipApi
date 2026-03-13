using MediatR;
using Mentorship.Shared.Contracts.v1.Sessions;

namespace Mentorship.Application.Features.Sessions.Queries.GetSession.GetAllSessions;

public record class GetAllSessionsQuery: IRequest<List<SessionResponse>>
{

}
