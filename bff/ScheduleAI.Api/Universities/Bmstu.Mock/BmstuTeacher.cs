using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu.Mock;

public class BmstuTeacher : IUniversityTeacher
{
    public required string Id { get; init; }
    public required string FullName { get; init; }

    public string[] Departments { get; init; } = [];
    
    internal List<BmstuPair> Pairs { get; } = [];
}