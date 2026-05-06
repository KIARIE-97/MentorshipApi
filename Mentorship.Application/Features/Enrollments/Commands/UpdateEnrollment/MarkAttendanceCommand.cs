using MediatR;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public record class MarkAttendanceCommand: IRequest<bool>
{
    public int EnrollmentId { get; init; }
    public int NumberOfSessions { get; init; } = 1; // Default to 1 session
}
