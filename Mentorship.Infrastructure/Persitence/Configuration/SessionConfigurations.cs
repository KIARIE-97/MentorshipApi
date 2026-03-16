using System;
using Mentorship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorship.Infrastructure.Persitence.Configuration;

public class SessionConfigurations: IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");
        
        builder.HasKey(s=>s.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(p => p.Description)
            .HasMaxLength(2000);
        
         builder.Property(s => s.Sessiontype)
            .IsRequired()
            .HasConversion<int>();  // Store enum as int in database
            
        builder.Property(s => s.ScheduleAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");  // PostgreSQL specific
            
        builder.Property(s => s.DurationMinutes)
            .IsRequired();
            
        // 4. Foreign Key relationship
        builder.HasOne(s => s.Program)
            .WithMany(p => p.Sessions)
            .HasForeignKey(s => s.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);  // Delete sessions when program is deleted
            
        // 5. Indexes for performance
        builder.HasIndex(s => s.ProgramId);  // Fast lookup by program
        builder.HasIndex(s => s.ScheduleAt); // Fast date-based queries
        builder.HasIndex(s => s.Sessiontype); // Fast filter by type    
    }

}
