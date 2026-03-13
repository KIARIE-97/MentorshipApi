using System;
using Mentorship.Api.Data;
using Mentorship.Core.Entities;
using Mentorship.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Infrastructure.Persitence.Repository;

public class SessionRepository(AppDbContext context): ISessionRepository
{
        private readonly AppDbContext _context = context;
    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
        .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<IEnumerable<Session>> GetAllAsync()
    {
        return await _context.Sessions
        .OrderByDescending(s=> s.ScheduleAt)
        .ToListAsync();
    }
    public async Task<IEnumerable<Session>> GetByProgramIdAsync(int id)
    {
        return await _context.Sessions
        .Where(s=> s.ProgramId == id)
        .ToListAsync();
    }
    public async Task<Session> AddAsync(Session session)
    {
         await _context.Sessions.AddAsync(session);
        return session;
    }
    public Task UpdateAsync(Session session)
    {
        _context.Entry(session).State = EntityState.Modified;
        return Task.CompletedTask;
    }
    public  Task DeleteAsync(Session session)
    {
        _context.Sessions.Remove(session);
        return Task.CompletedTask;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
