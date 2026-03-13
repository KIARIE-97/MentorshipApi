using System;
using Mentorship.Shared.Enums;
namespace Mentorship.Shared.Contracts.v1.Sessions;

public class CreateSessionRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ESessionType SessionType;
    public DateTime ScheduleAt { get; set; }
    public int DurationMinutes { get; set; }
    public int ProgramId { get; set; }
}
