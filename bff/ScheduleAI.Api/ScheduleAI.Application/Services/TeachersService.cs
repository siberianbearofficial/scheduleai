using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class TeachersService(IUniversityService universityService) : ITeachersService
{
    public async Task<IEnumerable<Teacher>> GetTeachersAsync(Guid universityId, string name)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.FindTeachers(name)).Select(UniversityTeacherToTeacher);
    }

    public async Task<IEnumerable<Teacher>> GetTeachersAsync(Guid universityId)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.GetTeachers()).Select(UniversityTeacherToTeacher);
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid universityId, string teacherId)
    {
        var university = universityService.GetUniversity(universityId);
        return UniversityTeacherToTeacher(await university.GetTeacherById(teacherId));
    }

    private static Teacher UniversityTeacherToTeacher(IUniversityTeacher universityTeacher)
    {
        return new Teacher
        {
            Id = universityTeacher.Id,
            FullName = universityTeacher.FullName,
        };
    }
}