using System;
using MediatR;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Sessions.Commands.DeleteSession;

public class DeleteSessionHandler( ISessionRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteSessionCommand, bool>
{
    private readonly ISessionRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(request.Id);
        if(session == null) return false;

        await _repository.DeleteAsync(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
