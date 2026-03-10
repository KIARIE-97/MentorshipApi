using MediatR;
using Mentorship.Shared.Contracts.v1.Programs;

namespace Mentorship.Application.Features.Programs.Commands.UpdateProgram;

public record class UpdateProgramCommand: IRequest<bool>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Id{get; init;}

}
