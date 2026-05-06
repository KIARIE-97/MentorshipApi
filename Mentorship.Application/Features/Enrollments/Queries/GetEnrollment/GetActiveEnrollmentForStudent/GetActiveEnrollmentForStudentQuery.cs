using MediatR;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetActiveEnrollmentForStudent;

public record class GetActiveEnrollmentForStudentQuery: IRequest<EnrollmentResponse?>
{
    public int StudentId { get; init; }
    public int ProgramId { get; init; }
}
