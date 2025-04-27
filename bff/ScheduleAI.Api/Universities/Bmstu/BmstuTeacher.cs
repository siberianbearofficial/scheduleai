using BmstuSchedule.Client.Models;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuTeacher : IUniversityTeacher
{
    public required string Id { get; init; }
    public required string FullName { get; init; }

    public string[] Departments { get; init; } = [];

    internal static BmstuTeacher FromModel(TeacherBase model)
    {
        return new BmstuTeacher()
        {
            Id = model.Id.ToString(),
            FullName = $"{model.LastName} {model.FirstName} {model.MiddleName}".Trim(),
            Departments = model.Departments,
        };
    }
}