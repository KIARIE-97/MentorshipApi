using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class MarkAttendanceHandler : IRequestHandler<MarkAttendanceCommand, bool>
{
    private readonly IEnrollmentRepository _repository;

    public MarkAttendanceHandler(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetByIdAsync(request.EnrollmentId);
        if (enrollment == null)
            return false;

        for (int i = 0; i < request.NumberOfSessions; i++)
        {
            enrollment.MarkAttendance();
        }

        await _repository.UpdateAsync(enrollment);
        return true;
    }
}
