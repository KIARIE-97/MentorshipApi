using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class CompleteEnrollmentHandler : IRequestHandler<CompleteEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _repository;

    public CompleteEnrollmentHandler(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CompleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetByIdAsync(request.EnrollmentId);
        if (enrollment == null)
            return false;

        enrollment.Complete(request.Grade, request.Feedback);
        await _repository.UpdateAsync(enrollment);
        return true;
    }
}
