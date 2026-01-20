using System;
using Mentorship.Api.Enums;

namespace Mentorship.Api.Dtos.Users;

public class UpdateUserDto
{
    public  string? FullName { get; set; } 
    public  string? PhoneNumber {get; set;}
    public  string? Email {get;set;} 
    public  string? Password {get;set;}
    public string? Bio {get;set;}

    //Authorization
    public RoleType? Role{get; set;} 
}
