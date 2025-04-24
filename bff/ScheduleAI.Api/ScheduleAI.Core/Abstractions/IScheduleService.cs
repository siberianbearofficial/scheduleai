using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IScheduleService
{
    public Task<IEnumerable<Pair>> GetGroupScheduleAsync(string universityId, string groupId, DateTime startDate,
        DateTime endDate);

    public Task<IEnumerable<Pair>> GetTeacherScheduleAsync(string universityId, string teacherId, DateTime startDate,
        DateTime endDate);

    public Task<IEnumerable<MergedPair>> GetMergedScheduleAsync(string universityId, string groupId, string teacherId,
        DateTime startDate, DateTime endDate);
}