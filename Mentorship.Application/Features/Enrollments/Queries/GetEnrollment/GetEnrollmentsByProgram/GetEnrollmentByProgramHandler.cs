using System;
using AutoMapper;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Enrollments;

namespace Mentorship.Application.Features.Enrollments.Queries.GetEnrollment.GetEnrollmentsByProgram;

public class GetEnrollmentByProgramHandler : IRequestHandler<GetEnrollmentsByProgramQuery, List<EnrollmentResponse>>
{
    private readonly IEnrollmentRepository _repository;
    private readonly IMapper _mapper;

    public GetEnrollmentByProgramHandler(IEnrollmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<EnrollmentResponse>> Handle(GetEnrollmentsByProgramQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _repository.GetByProgramIdAsync(request.ProgramId);
        return _mapper.Map<List<EnrollmentResponse>>(enrollments);
    }
}
