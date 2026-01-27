using System.ComponentModel.DataAnnotations;

namespace Mentorship.Api.Dtos.Session;

public record class CreateSession
{
  [Required]
  public required string SessionTitle{get;set;}
  [Required]
  [MaxLength (200)]
   public required string SessionDescription{get;set;}
   [Required]
   public DateTime ScheduleAt{get;set;}
}
