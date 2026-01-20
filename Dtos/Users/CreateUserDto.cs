using System;
using System.ComponentModel.DataAnnotations;

namespace Mentorship.Api.Dtos.Users;

public class CreateUserDto
{  
    [Required]
    public required string FullName { get; set; } 
    [Required]
    [MaxLength(10)]
    public required string PhoneNumber {get; set;}
    [Required]
    [EmailAddress]
    public required string Email {get;set;} 
    [Required]
    public required string Password {get;set;} 
}
