using System;
using MediatR;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Programs.Commands.CreateProgram;

public class CreateProgramHandler( IMentorshipProgramRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProgramCommand, int>
{
    private readonly IMentorshipProgramRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    

  public async Task<int> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        // 1. Create the domain entity (uses factory method)
        var program = MentorshipProgram.Create(
            request.Title,
            request.Description,
            request.StartDate
        );
        
        // 2. Save to database through repository
        await _repository.AddAsync(program);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // 3. Return the ID
        return program.Id;
    }
}
