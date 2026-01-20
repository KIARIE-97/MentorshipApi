using System;
using System.ComponentModel.DataAnnotations;

namespace Mentorship.Api.Dtos.Programs;

public class CreateProgramDto
{
    [Required]
     public required string Title {get; set;} = string.Empty;
     [Required]
     public required string Description {get; set;} = string.Empty;
     [Required]
     public required DateOnly Start {get; set;}
     public DateOnly? End {get; set;}
}
