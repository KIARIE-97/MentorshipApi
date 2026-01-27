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
    //relationships
    public User? User{get; set;}
    public ICollection<Session>? Sessions{get;set;}
}
