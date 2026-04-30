using System;

namespace Mentorship.Shared.Contracts.v1.Enrollments;

public class CreateEnrollment
{
    public int StudentId { get; set; }
    public int ProgramId { get; set; }

}
