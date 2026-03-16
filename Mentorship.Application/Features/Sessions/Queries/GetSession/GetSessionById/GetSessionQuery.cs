using MediatR;
using Mentorship.Shared.Contracts.v1.Sessions;

namespace Mentorship.Application.Features.Sessions.Queries.GetSession.GetSessionById;

public record class GetSessionQuery: IRequest<SessionResponse>
{
    public int Id{get; init;}
}
