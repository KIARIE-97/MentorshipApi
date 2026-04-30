using System;
using Mentorship.Api.Data;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Infrastructure.Persitence.Repository;

public class EnrollmentRepository(AppDbContext context): IEnrollmentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context
        .Enrollments
        .FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int id)
    {
         return await _context
        .Enrollments
        .Where(e => e.StudentId == id)
        .ToListAsync();
    }
     public async Task<IEnumerable<Enrollment>> GetByProgramIdAsync(int id)
    {
         return await _context
        .Enrollments
        .Where(e => e.ProgramId == id)
        .ToListAsync();
    }
    public async Task<IEnumerable<Enrollment>> GetActiveEnrollmentsAsync()
    {
         return await _context
         .Enrollments
         .Where(e => e.Status == Core.Enums.EnrollmentStatus.Active)
         .ToListAsync();
    }

    public async Task<Enrollment?> GetActiveEnrollmentForStudentAsync(int studentId, int programId)
    {
        return await _context
        .Enrollments
        .FirstOrDefaultAsync(e => e.StudentId == studentId 
                            && e.ProgramId == programId 
                            && e.Status == Core.Enums.EnrollmentStatus.Active);

    }
    public async Task<Enrollment> AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        return enrollment;
    }

    public Task UpdateAsync(Enrollment enrollment)
    {
        _context.Entry(enrollment).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int studentId, int programId)
    {
        return await _context.Enrollments.AnyAsync(e => e.StudentId == studentId 
                                                        && e.ProgramId == programId);
    }
    
}