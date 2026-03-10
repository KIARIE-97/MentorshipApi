using System;
using System.Buffers;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Programs;

namespace Mentorship.Application.Features.Programs.Queries.GetProgram.GetProgramById;

public class GetProgramHandler (IMentorshipProgramRepository repository) : IRequestHandler<GetProgramQuery, ProgramResponse?>
{
    private readonly IMentorshipProgramRepository _repository = repository;

    public async Task<ProgramResponse?> Handle(GetProgramQuery request, CancellationToken cancellationToken)
    {
        var program = await _repository.GetByIdAsync(request.Id);

        if(program == null) return null;

        return new ProgramResponse
        {
            Id = program.Id,
            Title = program.Title,
            Description = program.Description,
            StartDate = program.StartDate,
            EndDate = program.EndDate,
            CreatedAt = program.CreatedAt,
            // SessionCount = program.Sessions.Count,
            // UserId = program.UserId
        };
    }
}
