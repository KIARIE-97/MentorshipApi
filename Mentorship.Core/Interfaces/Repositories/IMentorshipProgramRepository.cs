using System;
using Mentorship.Core.Entities;

namespace Mentorship.Core.Interfaces.Repositories;

public interface IMentorshipProgramRepository
{
  //query methods
    Task<MentorshipProgram?> GetByIdAsync(int id);
    Task<IEnumerable<MentorshipProgram>> GetAllAsync();
    Task<IEnumerable<MentorshipProgram>> GetByUserAsync(int userId);
    Task<bool> ExistsAsync(int id);
    
    // Command methods
    Task<MentorshipProgram> AddAsync(MentorshipProgram program);
    Task UpdateAsync(MentorshipProgram program);
    Task DeleteAsync(MentorshipProgram program);
    
    // Save changes - often handled by Unit of Work
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
