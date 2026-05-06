using System;
using AutoMapper;
using MediatR;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentHandler : IRequestHandler<CreateEnrollmentCommand, EnrollmentResponse>
{
    private readonly IEnrollmentRepository _repository;
    private readonly IMapper _mapper;

    public CreateEnrollmentHandler(IEnrollmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EnrollmentResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        // Check if enrollment already exists
        var exists = await _repository.ExistsAsync(request.StudentId, request.ProgramId);
        if (exists)
        {
            throw new InvalidOperationException("Student is already enrolled in this program");
        }

        var enrollment = Enrollment.Create(request.StudentId, request.ProgramId);
       

        var created = await _repository.AddAsync(enrollment);
        return _mapper.Map<EnrollmentResponse>(created);
    }
}
