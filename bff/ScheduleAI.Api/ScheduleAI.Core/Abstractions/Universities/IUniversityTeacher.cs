namespace ScheduleAI.Core.Abstractions.Universities;

public interface IUniversityTeacher
{
    public string Id { get; }
    public string FullName { get; }
    public string[] Departments { get; }
}