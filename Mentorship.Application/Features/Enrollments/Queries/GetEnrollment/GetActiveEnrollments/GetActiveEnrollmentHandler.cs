using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetActiveEnrollments;

public class GetActiveEnrollmentHandler(IEnrollmentRepository repository): IRequestHandler<GetActiveEnrollmentQuery, List<EnrollmentResponse>?>
{
    private readonly IEnrollmentRepository _repository = repository;
    public async Task<List<EnrollmentResponse>?> Handle(GetActiveEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _repository.GetActiveEnrollmentsAsync();
        return enrollments.Select(enrollment => new EnrollmentResponse
        {
            Id= enrollment.Id,
            StudentId = enrollment.StudentId,
            // StudentName = enrollment.st
            ProgramId = enrollment.ProgramId,
            ProgramTitle = enrollment.Program?.Title ?? "Unknown Program",
            EnrolledAt = enrollment.EnrolledAt,
            Status = (Mentorship.Shared.Enums.EnrollmentStatus)enrollment.Status,
            SessionsAttended = enrollment.SessionsAttended,
            CompletedAt = enrollment.CompletedAt,
            FinalGrade = enrollment.FinalGrade,
            ProgressPercentage = enrollment.ProgressPercentage
        }).ToList();
    }
}
