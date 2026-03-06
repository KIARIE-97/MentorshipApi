using System;
using Mentorship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorship.Infrastructure.Persitence.Configuration;

public class MentorshipProgramConfigurations :IEntityTypeConfiguration<MentorshipProgram>
{
    public void Configure(EntityTypeBuilder<MentorshipProgram> builder)
    {
        builder.ToTable("MentorshipPrograms");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(p => p.Description)
            .HasMaxLength(2000);
            
        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
            
        // Relationships
        // builder.HasOne(p => p.User)
        //     .WithMany()
        //     .HasForeignKey(p => p.UserId)
        //     .OnDelete(DeleteBehavior.SetNull);
            
        // builder.HasMany(p => p.Sessions)
        //     .WithOne(s => s.Program)
        //     .HasForeignKey(s => s.ProgramId)
        //     .OnDelete(DeleteBehavior.Cascade);
            
        // Indexes for performance
        // builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.StartDate);
    }
}
