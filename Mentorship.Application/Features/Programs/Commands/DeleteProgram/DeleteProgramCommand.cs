using MediatR;

namespace Mentorship.Application.Features.Programs.Commands.DeleteProgram;

public record class DeleteProgramCommand: IRequest<bool>
{
    public int Id{get; init;}

    public  DeleteProgramCommand (int id)
    {
          Id = id; 
    }
}
