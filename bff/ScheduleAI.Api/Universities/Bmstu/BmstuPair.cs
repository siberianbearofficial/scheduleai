using BmstuSchedule.Client.Models;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuPair : IUniversityPair
{
    public required string TeacherId { get; init; }
    public required string GroupId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string? Room { get; init; } = string.Empty;
    
    public string? ActType { get; init; }
    public string? Discipline { get; init; }

    internal static BmstuPair FromApiModel(GroupScheduleItem groupScheduleItem, string teacherId, string groupId)
    {
        return new BmstuPair()
        {
            GroupId = groupId,
            TeacherId = teacherId,
            StartTime = groupScheduleItem.TimeSlot.StartTime,
            EndTime = groupScheduleItem.TimeSlot.EndTime,
            Room = groupScheduleItem.Room?.Name,
            ActType = groupScheduleItem.Discipline.ActType,
            Discipline = groupScheduleItem.Discipline.FullName,
        };
    }
}