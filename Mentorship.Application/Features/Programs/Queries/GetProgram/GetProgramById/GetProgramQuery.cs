using MediatR;
using Mentorship.Shared.Contracts.v1.Programs;

namespace Mentorship.Application.Features.Programs.Queries.GetProgram.GetProgramById;

public record class GetProgramQuery : IRequest<ProgramResponse>
{
    public int Id {get; init;}
}
