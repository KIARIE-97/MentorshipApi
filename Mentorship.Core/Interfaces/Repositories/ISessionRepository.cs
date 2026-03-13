using System;
using Mentorship.Core.Entities;

namespace Mentorship.Core.Interfaces.Repositories;

public interface ISessionRepository
{
    //query methods
    Task<Session>GetByIdAsync(int id);
    Task<IEnumerable<Session>> GetAllAsync();
    Task<IEnumerable<Session>> GetByProgramIdAsync(int programId);
    //command methods
    Task<Session> AddAsync(Session session);
    Task UpdateAsync(Session session);
    Task DeleteAsync(Session session);

    //save changes
    Task<int>SaveChangesAsync(CancellationToken cancellationToken);
}
