using MediatR;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetActiveEnrollments;

public record class GetActiveEnrollmentQuery: IRequest<List<EnrollmentResponse>>
{

}
