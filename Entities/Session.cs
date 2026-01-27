using System;
using Mentorship.Api.Enums;

namespace Mentorship.Api.Entities;

public class Session
{
    public int SessionId{get; set;} 
    public string SessionTitle{get;set;} = string.Empty;
    public string SessionDescription{get;set;} = string.Empty;
    public SessionType Sessiontype = SessionType.Online;
    public DateTime ScheduleAt{get;set;}
    public User? Attendee{get; set;}
    public MentorshipProgram? Program{get;set;}
}
