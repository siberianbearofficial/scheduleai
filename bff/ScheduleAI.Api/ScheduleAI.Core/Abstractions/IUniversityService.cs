using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IUniversityService
{
    public IUniversity GetUniversity(Guid universityId);
    public IEnumerable<University> GetAllUniversities();
}