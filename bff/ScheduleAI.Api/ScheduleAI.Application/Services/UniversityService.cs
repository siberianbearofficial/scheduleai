using Bmstu;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class UniversityService : IUniversityService
{
    private readonly Dictionary<Guid, IUniversity> _universities = [];

    public UniversityService()
    {
        AddUniversity(new BmstuUniversity());
    }

    private void AddUniversity(IUniversity university)
    {
        _universities.Add(Guid.NewGuid(), university);
    }

    public IUniversity GetUniversity(Guid universityId)
    {
        return _universities[universityId];
    }

    public IEnumerable<University> GetAllUniversities()
    {
        return _universities.Select(e => new University() { Id = e.Key, Name = e.Value.Name });
    }
}