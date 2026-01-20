using System;
using Mentorship.Api.Enums;

namespace Mentorship.Api.Entities;

public class Skill
{
    public Guid SkillId {get;set;}
    public string? SkillName {get; set;}
    public SkillCategory Category{get;set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public ICollection<UserSkill>? UserSkills{get; set;}
}
