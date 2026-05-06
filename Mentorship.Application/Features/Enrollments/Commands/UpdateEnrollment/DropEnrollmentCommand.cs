using MediatR;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public record class DropEnrollmentCommand: IRequest<bool>
{
    public int EnrollmentId { get; init; }
    public string? Reason { get; init; }
}
