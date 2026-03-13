using MediatR;
using Mentorship.Shared.Contracts.v1.Sessions;
using Mentorship.Shared.Enums;

namespace Mentorship.Application.Features.Sessions.Commands.CreateSession;

public record class CreateSessionCommand:IRequest<int>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ESessionType Sessiontype;
    public DateTime ScheduleAt { get; init; }
    public int DurationMinutes { get; init; }
    public int ProgramId { get; init; }

    public static CreateSessionCommand FromRequest(CreateSessionRequest request)
    {
        return new CreateSessionCommand
        {
          Title = request.Title,
          Description = request.Description,
          DurationMinutes = request.DurationMinutes,
          ScheduleAt = request.ScheduleAt,
          ProgramId = request.ProgramId , 
          Sessiontype = request.SessionType
        };
    }
}
