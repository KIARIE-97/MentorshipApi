using System;
using MediatR;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Sessions.Commands.UpdateSession;

public class UpdateSessionHandler( ISessionRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateSessionCommand, bool>
{
    private readonly ISessionRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<bool> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var currentSession = await _repository.GetByIdAsync(request.Id);
        if(currentSession == null) return false;
        

        currentSession.UpdateDetails(request.Title, request.Description, request.DurationMinutes);

        await _repository.UpdateAsync(currentSession);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }

}
