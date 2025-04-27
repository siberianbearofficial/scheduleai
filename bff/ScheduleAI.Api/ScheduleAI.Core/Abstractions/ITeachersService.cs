using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface ITeachersService
{
    public Task<Teacher> GetTeacherByIdAsync(string universityId, string teacherId);
    public Task<IEnumerable<Teacher>> GetTeachersAsync(string universityId);
    public Task<IEnumerable<Teacher>> GetTeachersAsync(string universityId, string name);
    public Task<IEnumerable<Teacher>> GetTeachersByGroupAsync(string universityId, string groupId);
}