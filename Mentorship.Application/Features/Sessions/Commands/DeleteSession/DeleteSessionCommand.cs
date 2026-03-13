using MediatR;

namespace Mentorship.Application.Features.Sessions.Commands.DeleteSession;

public record class DeleteSessionCommand: IRequest<bool>
{
    public int Id{get; init;}
    public DeleteSessionCommand(int id)
    {
        Id = id;
    }
}
