using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IUniversityService
{
    public IUniversity GetUniversity(string universityId);
    public IEnumerable<IUniversity> GetAllUniversities();
}