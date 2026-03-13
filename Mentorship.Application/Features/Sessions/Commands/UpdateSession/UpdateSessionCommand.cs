using MediatR;

namespace Mentorship.Application.Features.Sessions.Commands.UpdateSession;

public record class UpdateSessionCommand: IRequest<bool>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int DurationMinutes { get; init; }
    public int Id{get; init;}
}
