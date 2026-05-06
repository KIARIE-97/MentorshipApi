using MediatR;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetEnrollmentById;

public record class GetEnrollmentByIdQuery: IRequest<EnrollmentResponse?>
{
    public int Id { get; init; }
}
