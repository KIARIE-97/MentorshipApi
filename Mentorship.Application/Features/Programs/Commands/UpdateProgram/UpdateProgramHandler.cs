using System;
using MediatR;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Programs.Commands.UpdateProgram;

public class UpdateProgramHandler( IMentorshipProgramRepository  repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateProgramCommand, bool>
{
    private readonly IMentorshipProgramRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var currProgram = await _repository.GetByIdAsync(request.Id);
        if(currProgram == null) return false;
        

        currProgram.UpdateDetails(request.Title, request.Description);

        await _repository.UpdateAsync(currProgram);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}
