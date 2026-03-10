using System;
using MediatR;
using Mentorship.Core.Interfaces;
using Mentorship.Core.Interfaces.Repositories;

namespace Mentorship.Application.Features.Programs.Commands.DeleteProgram;

public class DeleteProgramHandler(IMentorshipProgramRepository repository, IUnitOfWork unitOfWork)  : IRequestHandler<DeleteProgramCommand, bool>
{
    private readonly IMentorshipProgramRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _repository.GetByIdAsync(request.Id);
        if(program == null) return false;

        await _repository.DeleteAsync(program);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
