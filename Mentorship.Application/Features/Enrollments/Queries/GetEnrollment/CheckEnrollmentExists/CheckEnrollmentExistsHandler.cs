using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.CheckEnrollmentExists;

public class CheckEnrollmentExistsHandler : IRequestHandler<CheckEnrollmentExistsQuery, bool>
{
    private readonly IEnrollmentRepository _repository;

    public CheckEnrollmentExistsHandler(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CheckEnrollmentExistsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.ExistsAsync(request.StudentId, request.ProgramId);
    }
}
