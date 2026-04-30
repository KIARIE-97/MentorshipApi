using System;
using Mentorship.Shared.Enums;

namespace Mentorship.Shared.Contracts.v1.Enrollments;

public class EnrollmentResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int ProgramId { get; set; }
    public string ProgramTitle { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public EnrollmentStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int SessionsAttended { get; set; }
    public int? FinalGrade { get; set; }
    public double ProgressPercentage { get; set; }

}
