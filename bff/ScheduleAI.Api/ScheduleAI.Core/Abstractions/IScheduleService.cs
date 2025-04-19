using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IScheduleService
{
    public Task<IEnumerable<Pair>> GetGroupScheduleAsync(Guid universityId, string groupId, DateTime startDate,
        DateTime endDate);

    public Task<IEnumerable<Pair>> GetTeacherScheduleAsync(Guid universityId, string teacherId, DateTime startDate,
        DateTime endDate);

    public Task<IEnumerable<MergedPair>> GetMergedScheduleAsync(Guid universityId, string groupId, string teacherId,
        DateTime startDate, DateTime endDate);
}