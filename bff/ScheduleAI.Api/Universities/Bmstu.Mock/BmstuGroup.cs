using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu.Mock;

public class BmstuGroup : IUniversityGroup
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    internal List<BmstuPair> Pairs { get; } = [];
}