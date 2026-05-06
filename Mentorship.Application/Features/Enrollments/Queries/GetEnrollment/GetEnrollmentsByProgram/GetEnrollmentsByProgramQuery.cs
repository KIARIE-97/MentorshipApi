using MediatR;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetEnrollmentsByProgram;

public record class GetEnrollmentsByProgramQuery: IRequest<List<EnrollmentResponse>>
{
    public int ProgramId { get; init; }
}
