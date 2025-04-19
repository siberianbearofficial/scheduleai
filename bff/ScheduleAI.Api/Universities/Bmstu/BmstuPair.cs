using BmstuSchedule.Client.Models;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuPair : IUniversityPair
{
    public required string[] Teachers { get; init; }
    public required string GroupId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string[] Rooms { get; init; } = [];

    public string? ActType { get; init; }
    public string? Discipline { get; init; }

    internal static BmstuPair FromApiModel(GroupScheduleItem groupScheduleItem)
    {
        return new BmstuPair()
        {
            GroupId = groupScheduleItem.GroopId,
            Teachers = groupScheduleItem.Teachers?.Select(x => x.Id.ToString()).ToArray() ?? [],
            StartTime = groupScheduleItem.TimeSlot.StartTime,
            EndTime = groupScheduleItem.TimeSlot.EndTime,
            Rooms = groupScheduleItem.Room?.Name,
            ActType = groupScheduleItem.Discipline.ActType,
            Discipline = groupScheduleItem.Discipline.FullName,
        };
    }
}