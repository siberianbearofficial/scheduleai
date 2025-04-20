using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface ITeachersService
{
    public Task<Teacher> GetTeacherByIdAsync(Guid universityId, string teacherId);
    public Task<IEnumerable<Teacher>> GetTeachersAsync(Guid universityId);
    public Task<IEnumerable<Teacher>> GetTeachersAsync(Guid universityId, string name);
    public Task<IEnumerable<Teacher>> GetTeachersByGroupAsync(Guid universityId, string groupId);
}