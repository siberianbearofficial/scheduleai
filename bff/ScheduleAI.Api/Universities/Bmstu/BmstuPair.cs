using BmstuSchedule.Client.Models;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuPair : IUniversityPair
{
    public required string[] Teachers { get; init; }
    public required string[] Groups { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string[] Rooms { get; init; } = [];

    public string? ActType { get; init; }
    public string? Discipline { get; init; }

    internal static BmstuPair FromApiModel(SchedulePairRead scheduleItem)
    {
        return new BmstuPair()
        {
            // Несколько дисциплин для одной пары - явно некорректно. Поэтому Single()
            Groups = scheduleItem.Groups.Select(e => e.Id.ToString()).ToArray(),
            Teachers = scheduleItem.Teachers?.Select(x => x.Id.ToString()).ToArray() ?? [],
            StartTime = scheduleItem.TimeSlot.StartTime,
            EndTime = scheduleItem.TimeSlot.EndTime,
            Rooms = scheduleItem.Rooms.Select(r => r.Name).ToArray(),
            ActType = scheduleItem.Disciplines.Single().ActType,
            Discipline = scheduleItem.Disciplines.Single().FullName,
        };
    }
}