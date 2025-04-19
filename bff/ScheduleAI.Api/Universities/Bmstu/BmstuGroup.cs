using BmstuSchedule.Client.Models;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuGroup : IUniversityGroup
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    internal static BmstuGroup FromModel(GroupBase model)
    {
        return new BmstuGroup()
        {
            Id = model.Id.ToString(),
            Name = model.Abbr
        };
    }
}