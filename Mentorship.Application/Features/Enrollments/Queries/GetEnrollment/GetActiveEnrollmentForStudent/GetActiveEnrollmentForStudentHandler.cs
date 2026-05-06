using System;
using AutoMapper;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetActiveEnrollmentForStudent;

public class GetActiveEnrollmentForStudentHandler : IRequestHandler<GetActiveEnrollmentForStudentQuery, EnrollmentResponse?>
{
    private readonly IEnrollmentRepository _repository;
    private readonly IMapper _mapper;

    public GetActiveEnrollmentForStudentHandler(IEnrollmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EnrollmentResponse?> Handle(GetActiveEnrollmentForStudentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetActiveEnrollmentForStudentAsync(request.StudentId, request.ProgramId);
        return enrollment == null ? null : _mapper.Map<EnrollmentResponse>(enrollment);
    }
}