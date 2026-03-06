namespace Mentorship.Shared.Contracts.v1.Programs;

public class CreateProgramRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
}