using System;
using Mentorship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentorship.Infrastructure.Persitence.Configuration;

public class EnrollmentConfiguration: IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        
        builder.HasKey(e => e.Id);
        
        // Indexes for performance
        builder.HasIndex(e => e.StudentId);
        builder.HasIndex(e => e.ProgramId);
        builder.HasIndex(e => e.Status);
        
        // Property configurations
        builder.Property(e => e.StudentId)
            .IsRequired();
            
        builder.Property(e => e.ProgramId)
            .IsRequired();
            
        builder.Property(e => e.EnrolledAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");
            
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();
            
        builder.Property(e => e.SessionsAttended)
            .HasDefaultValue(0);
            
        builder.Property(e => e.FinalGrade)
            .HasDefaultValue(null);
            
        // builder.Property(e => e.AmountPaid)
        //     .HasPrecision(18, 2);
            
        // // Relationships
        // builder.HasOne(e => e.Student)
        //     .WithMany()  // If User doesn't have an Enrollments collection
        //     .HasForeignKey(e => e.StudentId)
        //     .OnDelete(DeleteBehavior.Restrict);  // Don't delete enrollments if user is deleted
            
        builder.HasOne(e => e.Program)
            .WithMany(p => p.Enrollments)  // Add Enrollments collection to MentorshipProgram
            .HasForeignKey(e => e.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);  // Delete enrollments if program is deleted
    }

}
