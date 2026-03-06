using System;

namespace Mentorship.Shared.Contracts.v1.Programs;

public class ProgramResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? SessionCount { get; set; }
    
    // Optional: Include basic user info if needed
    public int? UserId { get; set; }
    public string? UserName { get; set; }
}
