using System;

namespace Mentorship.Api.Dtos.Programs;

public class UpdateProgramDto
{
     public string? Title {get; set;} 
     public string? Description {get; set;} 
     public DateOnly? Start {get; set;}
     public DateOnly? End {get; set;}
}
