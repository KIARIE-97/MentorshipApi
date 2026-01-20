using System;

namespace Mentorship.Api.Entities;

public class MentorshipProgram
{
     public int ProgramId { get; set; }
     public string Title {get; set;} = string.Empty;
     public string Description {get; set;} = string.Empty;
     public DateOnly Start {get; set;}
     public DateOnly? End {get; set;}
    public DateTime CreatedAt {get; set;}
    
    public User? User{get; set;}
}
