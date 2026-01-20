using System;
using Mentorship.Api.Enums;

namespace Mentorship.Api.Entities;

public class User
{
    //identity
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty;
    public string Email {get;set;} = string.Empty;

    //Authentication
    public string Password {get;set;} = string.Empty;

     // Profile
    public string Bio {get;set;} = string.Empty;

    //Authorization
    public RoleType Role{get; set;} = RoleType.Mentee;
    public Skill? Skill {get; set;}

    // Status & Tracking
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    //relationship
    public int ProgramId{get; set;}
    public MentorshipProgram? MentorshipProgram{get; set;}
     public ICollection<UserSkill>? UserSkills{get; set;}


    
}
