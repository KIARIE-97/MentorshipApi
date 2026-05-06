using MediatR;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Commands.CreateEnrollment;

public record class CreateEnrollmentCommand: IRequest<EnrollmentResponse>
{
    public int StudentId { get; init; }
    public int ProgramId { get; init; }
}
