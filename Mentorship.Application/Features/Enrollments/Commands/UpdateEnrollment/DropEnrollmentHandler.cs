using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class DropEnrollmentHandler : IRequestHandler<DropEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _repository;

    public DropEnrollmentHandler(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DropEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetByIdAsync(request.EnrollmentId);
        if (enrollment == null)
            return false;

        enrollment.DropOut(request.Reason);
        await _repository.UpdateAsync(enrollment);
        return true;
    }
}