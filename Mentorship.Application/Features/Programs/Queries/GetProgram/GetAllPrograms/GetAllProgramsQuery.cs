using MediatR;
using Mentorship.Core.Interfaces.Repositories;
using Mentorship.Shared.Contracts.v1.Programs;

namespace Mentorship.Application.Features.Programs.Queries.GetProgram.GetAllPrograms;

public record class GetAllProgramsQuery : IRequest<List<ProgramResponse>>
{
}
