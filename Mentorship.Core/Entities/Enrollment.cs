using System;
using Mentorship.Core.Enums;
using Mentorship.Core.Exceptions;

namespace Mentorship.Core.Entities;

public class Enrollment
{
        // Primary Key
    public int Id { get; private set; }
    
    // Foreign Keys (required)
    public int StudentId { get; private set; }      
    public int ProgramId { get; private set; }     
    
    // Navigation Properties
    // public User? Student { get; private set; }       // The mentee details
    public MentorshipProgram? Program { get; private set; } 
    
    // Enrollment Details
    public DateTime EnrolledAt { get; private set; }
    public EnrollmentStatus Status { get; private set; } // Current status
    public DateTime? CompletedAt { get; private set; } 
    public DateTime? DroppedAt { get; private set; } 
    
    // Progress Tracking (Optional but useful)
    public int? FinalGrade { get; private set; }      // 0-100 or null
    public string? Feedback { get; private set; }      // Mentor's feedback
    public int SessionsAttended { get; private set; }  // Count of attended sessions
    
    // Payment/Commercial (if applicable)
    // public decimal? AmountPaid { get; private set; }
    // public bool IsPaymentComplete { get; private set; }
    
    // Private constructor for EF Core
    private Enrollment() { }

        // Factory method - the ONLY way to create an enrollment
    public static Enrollment Create(
        int studentId,
        int programId
        // decimal? amountPaid = null
        )
    {
        // Validation
        if (studentId <= 0)
            throw new DomainException("Student ID is required");
            
        if (programId <= 0)
            throw new DomainException("Program ID is required");
        
        return new Enrollment
        {
            StudentId = studentId,
            ProgramId = programId,
            EnrolledAt = DateTime.UtcNow,
            Status = EnrollmentStatus.Active,
            SessionsAttended = 0
            // AmountPaid = amountPaid,
            // IsPaymentComplete = amountPaid.HasValue // Simplified logic
        };
    }

        public void MarkAttendance()
    {
        if (Status != EnrollmentStatus.Active)
            throw new DomainException("Cannot mark attendance for inactive enrollment");
            
        SessionsAttended++;
    }
    
    public void Complete(int? grade = null, string? feedback = null)
    {
        if (Status != EnrollmentStatus.Active)
            throw new DomainException("Only active enrollments can be completed");
            
        Status = EnrollmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        FinalGrade = grade;
        Feedback = feedback;
    }
    
    public void DropOut(string? reason = null)
    {
        if (Status == EnrollmentStatus.Completed)
            throw new DomainException("Cannot drop a completed enrollment");
            
        if (Status == EnrollmentStatus.Dropped)
            throw new DomainException("Enrollment already dropped");
            
        Status = EnrollmentStatus.Dropped;
        DroppedAt = DateTime.UtcNow;
        Feedback = reason; 
    }
    public bool IsActive => Status == EnrollmentStatus.Active;
    public bool IsCompleted => Status == EnrollmentStatus.Completed;

    public double ProgressPercentage
    {
        get
        {
            if (Program == null || !Program.Sessions.Any())
                return 0;
                
            return (double)SessionsAttended / Program.Sessions.Count * 100;
        }
    }
}
