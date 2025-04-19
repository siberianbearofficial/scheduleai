namespace ScheduleAI.Core.Models;

public class Teacher
{
    public required string Id { get; init; }
    public required string FullName { get; init; }
    public string[] Departments { get; init; } = [];
}