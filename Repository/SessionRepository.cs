using System;
using Mentorship.Api.Data;
using Mentorship.Api.Dtos.Session;
using Mentorship.Api.Entities;

namespace Mentorship.Api.Repository;

public class SessionRepository(AppDbContext _context)
{
  private readonly AppDbContext context =_context;

  public async Task<Session> CreateSession(CreateSession sessionDto)
    {
        var newSession = new Session
        {
            SessionTitle = sessionDto.SessionTitle,
            SessionDescription= sessionDto.SessionDescription ,
            ScheduleAt = sessionDto.ScheduleAt
        };
        context.Sessions.Add(newSession);
        await context.SaveChangesAsync();
        return newSession;
    }
}
