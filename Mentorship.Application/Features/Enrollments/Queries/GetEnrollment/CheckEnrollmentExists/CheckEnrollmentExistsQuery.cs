using MediatR;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.CheckEnrollmentExists;

public record class CheckEnrollmentExistsQuery: IRequest<bool>
{
    public int StudentId { get; init; }
    public int ProgramId { get; init; }
}
