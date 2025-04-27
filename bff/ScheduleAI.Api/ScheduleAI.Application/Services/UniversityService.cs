using Bmstu.Mock;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class UniversityService : IUniversityService
{
    private readonly Dictionary<string, IUniversity> _universities = [];

    public UniversityService()
    {
        AddUniversity(new BmstuUniversity());
    }

    private void AddUniversity(IUniversity university)
    {
        _universities.Add(university.Id, university);
    }

    public IUniversity GetUniversity(string universityId)
    {
        return _universities[universityId];
    }

    public IEnumerable<IUniversity> GetAllUniversities()
    {
        return _universities.Values;
    }
}