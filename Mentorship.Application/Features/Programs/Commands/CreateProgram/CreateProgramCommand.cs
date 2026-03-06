namespace Mentorship.Application.Features.Programs.Commands.CreateProgram;

using MediatR;
using Mentorship.Shared.Contracts.v1.Programs;

public record class CreateProgramCommand : IRequest<int>
{
 public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateOnly StartDate { get; init; }
    
    // Map from request to command
    public static CreateProgramCommand FromRequest(CreateProgramRequest request)
    {
        return new CreateProgramCommand
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate
        };
    }
}
