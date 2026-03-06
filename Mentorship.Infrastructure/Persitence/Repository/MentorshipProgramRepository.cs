using System;
using Mentorship.Api.Data;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Infrastructure.Persitence.Repository;

public class MentorshipProgramRepository(AppDbContext context) : IMentorshipProgramRepository
{
    private readonly AppDbContext _context = context;

    public async Task<MentorshipProgram?> GetByIdAsync( int id)
    {
        return await _context.Programs
        // .Include(p => p.Sessions)
        // .Include(p => p.User)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async  Task<IEnumerable<MentorshipProgram>> GetAllAsync()
    {
        return await _context.Programs
        // .Include(p => p.Sessions)
        .OrderByDescending(p => p.CreatedAt)
        .ToListAsync();
    }
    public async Task<IEnumerable<MentorshipProgram>> GetByUserAsync(int userId)
    {
        return await _context.Programs
            // .Where(p => p.UserId == userId)
            // .Include(p => p.Sessions)
            .ToListAsync();
    }
      public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Programs.AnyAsync(p => p.Id == id);
    }
     public async Task<MentorshipProgram> AddAsync(MentorshipProgram program)
    {
        await _context.Programs.AddAsync(program);
        return program;
    }
     public Task UpdateAsync(MentorshipProgram program)
    {
        _context.Entry(program).State = EntityState.Modified;
        return Task.CompletedTask;
    }
     public Task DeleteAsync(MentorshipProgram program)
    {
        _context.Programs.Remove(program);
        return Task.CompletedTask;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
