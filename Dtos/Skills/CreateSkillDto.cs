using System;
using System.ComponentModel.DataAnnotations;
using Mentorship.Api.Enums;

namespace Mentorship.Api.Dtos.Skills;

public class CreateSkillDto
{
    [Required]
    public required string SkillName {get; set;}
    [Required]
    public required SkillCategory Category{get;set;}
}
