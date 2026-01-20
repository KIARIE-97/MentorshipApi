using System;
using Mentorship.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Api.Data;

//use of primary constructor
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext (options) 
{
    public DbSet<User> Users {get; set;}
    public DbSet<MentorshipProgram> Programs {get; set;}
    public DbSet<Skill> Skills{get; set;}
}
