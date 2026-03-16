using Mentorship.Core.Enums;
using Mentorship.Core.Exceptions;

namespace Mentorship.Core.Entities;

public class Session
{
    public int Id{get; private set;} 
    public string Title{get;private set;} = string.Empty;
    public string Description{get;private set;} = string.Empty;
    public ESessionType Sessiontype {get; private set;}
    public DateTime ScheduleAt{get;private set;}
    public int DurationMinutes { get; private set; }
    public int ProgramId { get; private set; }
    public MentorshipProgram? Program { get; private set; }
    // public User? Attendee{get; private set;}

    private Session() {}

    public static Session Create(
        string title, string description, DateTime scheduleAt, int durationMinutes, int programId
    )
    {
        if(string.IsNullOrWhiteSpace(title)) throw new DomainException("session title is required");
        if(title.Length > 200) throw new DomainException("session title cannot exceed 200 characters");
        if(scheduleAt < DateTime.UtcNow) throw new DomainException("schedule time cannot be in the past");
        if (durationMinutes < 15 || durationMinutes > 480)
            throw new DomainException("Duration must be between 15 minutes and 8 hours");        
        
        return new Session
        {
          Title = title,
          Description = description,
          ScheduleAt = scheduleAt,
          DurationMinutes= durationMinutes,
          ProgramId = programId,
          Sessiontype = ESessionType.Online  
        };
    }

    //behaviour methods
    public void UpdateDetails (string title, string description, int durationMinutes)
       {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Program title is required");
            
        Title = title;
        Description = description ?? string.Empty;
        DurationMinutes = durationMinutes;
        // Sessiontype = sessionType;
    }
    public void Reschedule( DateTime newScheduleTime)
    {
        if(newScheduleTime < DateTime.UtcNow) throw new DomainException("the new schedule time cannot be in the past");
        ScheduleAt = newScheduleTime;
    }
    //sessiontype
}
