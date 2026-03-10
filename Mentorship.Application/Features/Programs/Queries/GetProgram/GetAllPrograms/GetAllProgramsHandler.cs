using System;
using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Programs;

namespace Mentorship.Application.Features.Programs.Queries.GetProgram.GetAllPrograms;

public class GetAllProgramsHandler(IMentorshipProgramRepository repository): IRequestHandler<GetAllProgramsQuery, List<ProgramResponse>?>
{
    private readonly IMentorshipProgramRepository _repository = repository;

    public async Task<List<ProgramResponse>?> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
    {
        var programs = await _repository.GetAllAsync();

        
        return programs.Select(program => new ProgramResponse
        {
            Id = program.Id,
            Title = program.Title,
            Description = program.Description,
            StartDate = program.StartDate,
            EndDate = program.EndDate,
            CreatedAt = program.CreatedAt,
            // SessionCount = program.Sessions.Count,
            // UserId = program.UserId
        }).ToList();
    }
}
