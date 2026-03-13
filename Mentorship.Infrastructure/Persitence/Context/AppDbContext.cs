
using Mentorship.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Api.Data;

//use of primary constructor
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext (options) 
{
    // public DbSet<User> Users {get; set;}
    public DbSet<MentorshipProgram> Programs {get; set;}
    // public DbSet<Skill> Skills{get; set;}
    public DbSet<Session> Sessions{get;set;}
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
