using System;
using MediatR;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Sessions.Commands.CreateSession;

public class CreateSessionHandler( ISessionRepository repository, IMentorshipProgramRepository programRepo, IUnitOfWork unitOfWork) : IRequestHandler<CreateSessionCommand, int>
{
    private readonly ISessionRepository _repository = repository;
    private readonly IMentorshipProgramRepository _programRepo = programRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var program = await _programRepo.GetByIdAsync(request.ProgramId);
        // if (program == null) ;
        
        // 1. Create the domain entity (uses factory method)
        var session = Session.Create(
            request.Title,
            request.Description,
            request.ScheduleAt,
            request.DurationMinutes,
            request.ProgramId        
            );
        
        // 2. Save to database through repository
        await _repository.AddAsync(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // 3. Return the ID
        return session.Id;
    }
}
