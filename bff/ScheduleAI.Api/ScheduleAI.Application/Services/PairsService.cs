using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class PairsService(IUniversityService universityService) : IScheduleService
{
    public async Task<IEnumerable<Pair>> GetGroupScheduleAsync(Guid universityId, string groupId, DateTime startDate,
        DateTime endDate)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.GetGroupSchedule(groupId, startDate, endDate)).Select(UniversityPairToPair);
    }

    public async Task<IEnumerable<Pair>> GetTeacherScheduleAsync(Guid universityId, string teacherId, DateTime startDate,
        DateTime endDate)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.GetTeacherSchedule(teacherId, startDate, endDate)).Select(UniversityPairToPair);
    }

    private static Pair UniversityPairToPair(IUniversityPair universityPair)
    {
        return new Pair
        {
            StartTime = universityPair.StartTime,
            EndTime = universityPair.EndTime,
            Discipline = universityPair.Discipline,
            ActType = universityPair.ActType,
            Teachers = universityPair.Teachers,
            Groups = universityPair.Groups,
            Rooms = universityPair.Rooms,
        };
    }

    public async Task<IEnumerable<MergedPair>> GetMergedScheduleAsync(Guid universityId, string groupId,
        string teacherId,
        DateTime startDate, DateTime endDate)
    {
        var studentPairs = (await GetGroupScheduleAsync(universityId, groupId, startDate, endDate))
            .GroupBy(e => e.StartTime.Date);
        var teacherPairs = (await GetTeacherScheduleAsync(universityId, teacherId, startDate, endDate))
            .GroupBy(e => e.StartTime.Date);

        var mergedPairs = new List<MergedPair>();
        foreach (var pairsOfDate in studentPairs
                     .Join(teacherPairs, e => e.Key, e => e.Key,
                         (st, tch) => new { Date = st.Key, StudentPairs = st, TeacherPairs = tch }))
        {
            foreach (var mergedPair in MergePairs(pairsOfDate.StudentPairs.ToArray(),
                         pairsOfDate.TeacherPairs.ToArray()))
            {
                mergedPairs.Add(mergedPair);
            }
        }

        return mergedPairs;
    }

    private List<MergedPair> MergePairs(Pair[] studentPairs,
        Pair[] teacherPairs)
    {
        var result = new List<MergedPair>();

        var lessonsStart = studentPairs.Select(e => e.StartTime).Min();
        var lessonsEnd = studentPairs.Select(e => e.EndTime).Max();

        foreach (var teacherPair in teacherPairs.Where(e => e.EndTime <= lessonsStart))
        {
            result.Add(MergedPairFromTeacherPair(teacherPair, MergedPairStatus.BeforePairs,
                waitTime: lessonsStart - teacherPair.EndTime));
        }

        foreach (var teacherPair in teacherPairs.Where(e => e.StartTime >= lessonsEnd))
        {
            result.Add(MergedPairFromTeacherPair(teacherPair, MergedPairStatus.AfterPairs,
                waitTime: teacherPair.StartTime - lessonsEnd));
        }

        var otherPairs = teacherPairs.Where(e => e.EndTime > lessonsStart || e.StartTime < lessonsEnd);
        var pairCollisions = otherPairs.Select(e => new
        {
            TeacherPair = e,
            StudentCollisions = studentPairs
                .Where(p => e.StartTime <= p.StartTime && p.StartTime <= e.EndTime ||
                            e.StartTime <= p.EndTime && p.EndTime <= e.EndTime)
        });

        foreach (var collision in pairCollisions)
        {
            result.Add(MergedPairFromTeacherPair(collision.TeacherPair,
                collision.StudentCollisions.Any() ? MergedPairStatus.Collision : MergedPairStatus.InGap,
                collisions: collision.StudentCollisions));
        }

        return result;
    }

    private static TimeSpan MaxWaitTime { get; } = TimeSpan.FromHours(8);

    private MergedPair MergedPairFromTeacherPair(Pair pair, MergedPairStatus status,
        IEnumerable<Pair>? collisions = null, TimeSpan? waitTime = null)
    {
        collisions = collisions?.ToArray();
        var convenience = status switch
        {
            MergedPairStatus.BeforePairs => 1 - waitTime / MaxWaitTime,
            MergedPairStatus.AfterPairs => 1 - waitTime / MaxWaitTime,
            MergedPairStatus.InGap => 1,
            MergedPairStatus.Collision => collisions?.Count() > 1 ? 0 : 0.5,
            _ => 0
        } ?? 0;
        return new MergedPair()
        {
            ActType = pair.ActType,
            Discipline = pair.Discipline,
            StartTime = pair.StartTime,
            EndTime = pair.EndTime,
            Rooms = pair.Rooms,
            Status = status,
            Collisions = collisions?.ToArray() ?? [],
            WaitTime = waitTime,
            Convenience = convenience
        };
    }
}