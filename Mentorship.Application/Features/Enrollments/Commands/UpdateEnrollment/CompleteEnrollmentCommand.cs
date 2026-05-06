using MediatR;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public record class CompleteEnrollmentCommand: IRequest<bool>
{
    public int EnrollmentId { get; init; }
    public int? Grade { get; init; }
    public string? Feedback { get; init; }
}
