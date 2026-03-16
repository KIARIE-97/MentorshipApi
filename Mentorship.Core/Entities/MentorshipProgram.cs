using Mentorship.Core.Exceptions;

namespace Mentorship.Core.Entities;

public class MentorshipProgram
{
     public int Id { get; private set; }
     public string Title {get; private set;} = string.Empty;
     public string Description {get; private set;} = string.Empty;
     public DateOnly StartDate {get; private set;}
     public DateOnly? EndDate {get; private set;}
    public DateTime CreatedAt {get; private set;}
    //relationships
    public int userid {get; private set;}
    // public User? User{get; private set;}
    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();


    //private constructor for ef core
    private MentorshipProgram() {}

    //factory method to create a program
    public static MentorshipProgram Create (
        string title,
        string description,
        DateOnly startDate,
        int? userId = null
    )
    {
        //validate
        if(string.IsNullOrWhiteSpace(title)) throw new DomainException("program title is required");
        if(title.Length > 200) throw new DomainException("Program title cannot exceed 200 characters");
        if(startDate < DateOnly.FromDateTime(DateTime.Today)) throw new DomainException("Start date cannot be in the past");

    return new MentorshipProgram
    {
        Title = title,
        Description = description,
        StartDate = startDate,
        CreatedAt = DateTime.UtcNow,
        // UserId = userId
    };
    }

    //behaviour methods
    public void UpdateDetails (string title, string description)
       {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Program title is required");
            
        Title = title;
        Description = description ?? string.Empty;
    }
      public void SetEndDate(DateOnly endDate)
    {
        if (endDate < StartDate)
            throw new DomainException("End date cannot be before start date");
            
        EndDate = endDate;
    }
    
    public void AddSession(Session session)
    {
        // Business rule: No overlapping sessions
        if (_sessions.Any(s => s.ScheduleAt == session.ScheduleAt))
            throw new DomainException("A session already exists at this time");
        if(session.ProgramId != Id && Id != 0) 
            throw new DomainException("session belongs to a different program");    
            
        _sessions.Add(session);
    }
    //remove session
    public void RemoveSession(Session session)
    {
        if(!_sessions.Contains(session))
            throw new DomainException("session not found in this program");
        _sessions.Remove(session);    
    }
    //get upcoming sessions
    public IReadOnlyCollection<Session> GetUpComingSessions()
    {
        return _sessions
        .Where(s => s.ScheduleAt > DateTime.UtcNow)
        .OrderBy(s=> s.ScheduleAt)
        .ToList()
        .AsReadOnly();
    }
    //check if program has sessions on a particular date
    public bool HasSessionOnDate(DateOnly date)
    {
        return _sessions.Any(s => DateOnly.FromDateTime(s.ScheduleAt) == date);
    }
}