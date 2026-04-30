using System;
using Mentorship.Core.Entities;

namespace Mentorship.Core.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Enrollment>> GetByProgramIdAsync(int programId);
    Task<IEnumerable<Enrollment>> GetActiveEnrollmentsAsync();
    Task<Enrollment?> GetActiveEnrollmentForStudentAsync(int studentId, int programId);
    Task<Enrollment> AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task<bool> ExistsAsync(int studentId, int programId);

}
